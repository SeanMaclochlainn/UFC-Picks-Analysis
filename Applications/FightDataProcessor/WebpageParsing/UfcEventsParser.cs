using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.WebpageParsing
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
