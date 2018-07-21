using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.WebpageParsing
{
    public class EventsDataExtractor
    {
        private EventFinder eventFinder;

        public EventsDataExtractor()
        {
            eventFinder = new EventFinder();
        }

        public void ParseAllEvents()
        {
            foreach (UfcEvent ufcEvent in eventFinder.GetAllEvents())
            {
                EventWebpagesDataExtractor eventWebpagesDataExractor = new EventWebpagesDataExtractor(ufcEvent);
                eventWebpagesDataExractor.ParseAllWebpages();
            }
        }
    }
}
