using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.ResultsPage;

namespace FightDataProcessor.WebpageParsing
{
    public class EventWebpagesParser
    {
        private UfcEvent ufcEvent;

        public EventWebpagesParser(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ParseAllWebpages()
        {
            ParseWikipediaPage();
        }

        private void ParseWikipediaPage()
        {
            Parser pageParser = new Parser(new HtmlDocumentGenerator(ufcEvent.GetWikipediaPage()).HtmlDocument, ufcEvent);
            pageParser.ParseResultsTableRows();
        }

    }
}
