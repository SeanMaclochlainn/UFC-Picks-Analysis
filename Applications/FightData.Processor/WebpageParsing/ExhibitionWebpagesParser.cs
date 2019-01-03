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

namespace FightDataProcessor.WebpageParsing
{
    public class ExhibitionWebpagesParser
    {
        private Exhibition exhibition;
        private EntityFinder entityFinder;
        private FightPicksContext context;
        private WebpageUpdater webpageUpdater;

        public ExhibitionWebpagesParser(FightPicksContext context)
        {
            this.context = context;
            entityFinder = new EntityFinder(context);
            webpageUpdater = new WebpageUpdater(context);
        }

        public RawExhibitionData ParseAllWebpages(Exhibition exhibition)
        {
            this.exhibition = exhibition;
            RawResultsPageData rawResultsPageData = ExtractResultsPageData();
            List<RawAnalystPick> rawAnalystPicks = ExtractPicksPagesData();
            List<RawFighterOdds> rawFighterOdds = ExtractOddsPageData();
            return new RawExhibitionData(rawResultsPageData, rawAnalystPicks, rawFighterOdds);
        }

        private RawResultsPageData ExtractResultsPageData()
        {
            List<RawFightResult> rawFightResults = new List<RawFightResult>();
            string date = "";
            Webpage resultsPage = entityFinder.WebpageFinder.GetResultsPage(exhibition);
            if (!resultsPage.Parsed)
            {
                ResultsPageParser resultsPageParser = new ResultsPageParser(new HtmlPageParser(resultsPage.Data).ParseHtml());
                rawFightResults = resultsPageParser.ParseResultTable();
                date = resultsPageParser.ParseDate();
                webpageUpdater.MarkAsParsed(resultsPage);
            }
            return new RawResultsPageData(rawFightResults, date);
        }

        private List<RawAnalystPick> ExtractPicksPagesData()
        {
            List<RawAnalystPick> rawAnalystPicks = new List<RawAnalystPick>();
            foreach (Webpage picksPage in entityFinder.WebpageFinder.GetPicksPages(exhibition))
            {
                if (!picksPage.Parsed)
                {
                    PicksPageParser rawAnalystPickList = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());
                    PicksPageConfiguration picksPageConfiguration = entityFinder.PicksPageConfigurationFinder.FindConfiguration(picksPage.Website);
                    rawAnalystPicks.AddRange(rawAnalystPickList.ParsePicksPage(picksPageConfiguration));
                    webpageUpdater.MarkAsParsed(picksPage);
                }
            }
            return rawAnalystPicks;
        }

        private List<RawFighterOdds> ExtractOddsPageData()
        {
            List<RawFighterOdds> rawFighterOdds = new List<RawFighterOdds>();
            Webpage oddsPage = entityFinder.WebpageFinder.GetOddsPage(exhibition);
            if (!oddsPage.Parsed)
            {
                OddsPageParser oddsPageParser = new OddsPageParser(new HtmlPageParser(oddsPage.Data).ParseHtml());
                rawFighterOdds = oddsPageParser.Parse();
                webpageUpdater.MarkAsParsed(oddsPage);
            }
            return rawFighterOdds;
        }
    }
}
