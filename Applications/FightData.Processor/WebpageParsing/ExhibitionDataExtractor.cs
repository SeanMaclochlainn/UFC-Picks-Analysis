using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Diagnostics;

namespace FightDataProcessor.WebpageParsing
{
    public class ExhibitionDataExtractor
    {
        private Exhibition exhibition;
        private WebpageFinder webpageFinder;
        private FightPicksContext context;
        private PickUpdater pickUpdater;
        private OddUpdater oddUpdater;

        public ExhibitionDataExtractor(Exhibition exhibition)
        {
            this.exhibition = exhibition;
            context = exhibition.Context;
            webpageFinder = new WebpageFinder(exhibition.Context);
            pickUpdater = new PickUpdater(context);
            oddUpdater = new OddUpdater(context);
        }

        public List<UnfoundPick> UnfoundPicks { get; private set; } = new List<UnfoundPick>();

        public void ExtractAllWebpages()
        {
            ExtractResultsPageData();
            ExtractPicksPagesData();
            ExtractOddsPageData();
        }

        private void ExtractResultsPageData()
        {
            Webpage resultsPage = webpageFinder.GetResultsPage(exhibition);
            if (!resultsPage.Parsed)
            {
                Debug.WriteLine($"Parsing results page {resultsPage.Url}");
                ResultsPageParser resultsPageParser = new ResultsPageParser(new HtmlPageParser(resultsPage.Data).ParseHtml());
                new FightUpdater(context).AddFights(resultsPageParser.ParseResultTable(), exhibition);
                new WebpageUpdater(context).MarkAsParsed(resultsPage);
            }
        }

        private void ExtractPicksPagesData()
        {
            foreach (Webpage picksPage in webpageFinder.GetPicksPages(exhibition))
            {
                if (!picksPage.Parsed)
                {
                    Debug.WriteLine($"Parsing picks page {picksPage.Url}");
                    List<RawAnalystPick> rawAnalystPickList = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml()).ParsePicksGrid();
                    RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
                    rawPickEvaluator.EvaluatePicks(rawAnalystPickList, exhibition);
                    pickUpdater.AddPicks(rawPickEvaluator.ValidPicks);
                    UnfoundPicks.AddRange(rawPickEvaluator.UnfoundPicks);
                    new WebpageUpdater(context).MarkAsParsed(picksPage);
                }
            }
        }

        private void ExtractOddsPageData()
        {
            Webpage oddsPage = webpageFinder.GetOddsPage(exhibition);
            if(!oddsPage.Parsed)
            {
                OddsPageParser oddsPageParser = new OddsPageParser(new HtmlPageParser(oddsPage.Data).ParseHtml());
                List<RawFighterOdds> rawFighterOdds = oddsPageParser.Parse();
                RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);
                List<Odd> odds = rawFighterOddsEvaluator.GetOdds(rawFighterOdds, exhibition);
                oddUpdater.AddOdds(odds);
            }
        }
    }
}
