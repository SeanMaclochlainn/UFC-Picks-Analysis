using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPage;
using FightDataProcessor.WebpageParsing.ResultsPage;
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

        }

        private void ParseResultsPage()
        {
            ResultsPageFightExtractor resultsPageDataExtractor = new ResultsPageFightExtractor(ufcEvent, context);
            resultsPageDataExtractor.ExtractFights();
        }

        private void ParseAllPicksPages()
        {
            
            PicksGridParser picksGridParser = new PicksGridParser(HtmlDocumentGenerator.FromWebpage(ufcEvent.Webpages.Last()), ufcEvent, context);
        }

    }
}
