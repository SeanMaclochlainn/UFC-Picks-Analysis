using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPage;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class EventWebpagesDataExtractor
    {
        private UfcEvent ufcEvent;
        private FightPicksContext context;

        public EventWebpagesDataExtractor(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.ufcEvent = ufcEvent;
            this.context = context;
        }

        public void ParseAllWebpages()
        {
            ParseResultsPage();
            ParseAllPicksPages();
        }

        private void ParseResultsPage()
        {
            ResultsPageFightExtractor resultsPageDataExtractor = new ResultsPageFightExtractor(ufcEvent, context);
            resultsPageDataExtractor.ExtractFights();
        }

        private void ParseAllPicksPages()
        {
            List<Webpage> picksPages = ufcEvent.Webpages.Where(w => w.WebpageType == WebpageType.PicksPage).ToList();
            foreach (Webpage picksPage in picksPages)
            {
                PicksPageDataExtractor picksPageDataExtractor = new PicksPageDataExtractor(XDocumentGenerator.FromWebpage(picksPage), ufcEvent, context);
                picksPageDataExtractor.ExtractGridData();
            }
        }

    }
}
