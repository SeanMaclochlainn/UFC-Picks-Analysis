using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.WebpageParsing
{
    public class EventsParser
    {
        private EventFinder eventFinder;

        public EventsParser()
        {
            eventFinder = new EventFinder();
        }

        public void ParseAllEvents()
        {
            foreach (UfcEvent ufcEvent in eventFinder.GetAllEvents())
            {
                EventWebpagesParser eventWebpagesDataCollector = new EventWebpagesParser(ufcEvent);
                eventWebpagesDataCollector.ParseAllWebpages();
            }
        }
    }
}
