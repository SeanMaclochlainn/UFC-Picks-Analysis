using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
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

        public ExhibitionDataExtractor(Exhibition exhibition)
        {
            this.exhibition = exhibition;
            context = exhibition.Context;
            webpageFinder = new WebpageFinder(exhibition.Context);
            pickUpdater = new PickUpdater(context);
        }

        public List<RawAnalystPick> InvalidPicks { get; private set; } = new List<RawAnalystPick>();

        public void ExtractAllWebpages()
        {
            ExtractResultsPageData();
            ExtractPicksPagesData();
        }

        public void ExtractResultsPageData()
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

        public void ExtractPicksPagesData()
        {
            foreach (Webpage picksPage in webpageFinder.GetPicksPages(exhibition))
            {
                if (!picksPage.Parsed)
                {
                    Debug.WriteLine($"Parsing picks page {picksPage.Url}");
                    List<RawAnalystPick> rawAnalystsPicks = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml()).ParsePicksGrid();
                    RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
                    rawPickEvaluator.EvaluatePicks(rawAnalystsPicks, exhibition);
                    pickUpdater.AddPicks(rawPickEvaluator.ValidPicks);
                    InvalidPicks.AddRange(rawPickEvaluator.InvalidPicks);
                    new WebpageUpdater(context).MarkAsParsed(picksPage);
                }
            }
        }
    }
}
