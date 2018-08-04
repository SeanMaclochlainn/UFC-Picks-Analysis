using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.FightData.Domain;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class EventDataExtractor
    {
        private UfcEvent ufcEvent;

        public EventDataExtractor(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ExtractAllWebpages()
        {
            ExtractResultsPage();
            ExtractAllPicksPages();
        }

        public void ExtractResultsPage()
        {
            ResultsPageParser resultsPageParser = new ResultsPageParser(XDocument.Parse(ufcEvent.GetResultsPage().Data));
            FightAdder fightAdder = new FightAdder(ufcEvent);
            fightAdder.AddFights(resultsPageParser.ParseTableRows());
            ufcEvent.Update();
        }

        private void ExtractAllPicksPages()
        {
            PicksPagesDataExtractor picksPagesDataExtractor = new PicksPagesDataExtractor(ufcEvent);
            picksPagesDataExtractor.ExtractAllPages();
        }

    }
}
