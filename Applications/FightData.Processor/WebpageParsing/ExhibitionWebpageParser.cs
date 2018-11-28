using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Diagnostics;

namespace FightDataProcessor.WebpageParsing
{
    public class ExhibitionWebpageParser
    {
        private Exhibition exhibition;
        private WebpageFinder webpageFinder;
        private FightPicksContext context;
        private WebpageUpdater webpageUpdater;

        public ExhibitionWebpageParser(FightPicksContext context)
        {
            this.context = context;
            webpageFinder = new WebpageFinder(context);
            webpageUpdater = new WebpageUpdater(context);
        }

        public RawExhibitionEntities ParseAllWebpages(Exhibition exhibition)
        {
            this.exhibition = exhibition;
            List<RawFightResult> rawFightResults = ExtractResultsPageData();
            List<RawAnalystPick> rawAnalystPicks = ExtractPicksPagesData();
            List<RawFighterOdds> rawFighterOdds = ExtractOddsPageData();
            return new RawExhibitionEntities(rawFightResults, rawAnalystPicks, rawFighterOdds);
        }

        private List<RawFightResult> ExtractResultsPageData()
        {
            List<RawFightResult> rawFightResults = new List<RawFightResult>();
            Webpage resultsPage = webpageFinder.GetResultsPage(exhibition);
            if (!resultsPage.Parsed)
            {
                Debug.WriteLine($"Parsing results page {resultsPage.Url}");
                ResultsPageParser resultsPageParser = new ResultsPageParser(new HtmlPageParser(resultsPage.Data).ParseHtml());
                rawFightResults = resultsPageParser.ParseResultTable();
                webpageUpdater.MarkAsParsed(resultsPage);
            }
            return rawFightResults;
        }

        private List<RawAnalystPick> ExtractPicksPagesData()
        {
            List<RawAnalystPick> rawAnalystPicks = new List<RawAnalystPick>();
            foreach (Webpage picksPage in webpageFinder.GetPicksPages(exhibition))
            {
                if (!picksPage.Parsed)
                {
                    Debug.WriteLine($"Parsing picks page {picksPage.Url}");
                    PicksPageParser rawAnalystPickList = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());
                    rawAnalystPicks.AddRange(rawAnalystPickList.ParsePicksGrid());
                    webpageUpdater.MarkAsParsed(picksPage);
                }
            }
            return rawAnalystPicks;
        }

        private List<RawFighterOdds> ExtractOddsPageData()
        {
            List<RawFighterOdds> rawFighterOdds = new List<RawFighterOdds>();
            Webpage oddsPage = webpageFinder.GetOddsPage(exhibition);
            if(!oddsPage.Parsed)
            {
                OddsPageParser oddsPageParser = new OddsPageParser(new HtmlPageParser(oddsPage.Data).ParseHtml());
                rawFighterOdds = oddsPageParser.Parse();
                webpageUpdater.MarkAsParsed(oddsPage);
            }
            return rawFighterOdds;
        }
    }
}
