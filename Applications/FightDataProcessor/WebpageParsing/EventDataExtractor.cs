using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;

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
            ExtractResultsPageData();
            ExtractPicksPagesData();
        }

        public void ExtractResultsPageData()
        {
            ResultsPageParser resultsPageParser = new ResultsPageParser(ufcEvent.GetResultsPage().GetHtml());
            new FightAdder(ufcEvent).AddFights(resultsPageParser.ParseTableRows());
            ufcEvent.Update();
        }

        public void ExtractPicksPagesData()
        {
            PickAdder pickAdder = new PickAdder(ufcEvent); 
            foreach (Webpage picksPage in ufcEvent.GetPicksPages())
            {
                List<RawUfcEventPicks> rawUfcEventPicks = new PicksPageGridParser(picksPage.GetHtml()).ParseRows();
                pickAdder.AddPicks(rawUfcEventPicks);
            }
        }
    }
}
