using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WikipediaParser;

namespace FightDataProcessor
{
    public class UfcEventsParser
    {
        private UfcEventFinder ufcEventFinder;

        public UfcEventsParser()
        {
            ufcEventFinder = new UfcEventFinder();
        }

        public void ParseAllEvents()
        {
            foreach (UfcEvent ufcEvent in ufcEventFinder.GetAllEvents())
            {
                EventWebpagesParser eventWebpagesDataCollector = new EventWebpagesParser(ufcEvent);
                eventWebpagesDataCollector.ParseAllWebpages();
            }
        }
    }
}
