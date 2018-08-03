using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.WebpageParsing
{
    public class EventsDataExtractor
    {
        private EventFinder eventFinder;

        public EventsDataExtractor(FightPicksContext context)
        {
            eventFinder = new EventFinder(context);
        }

        public void ExtractAllEvents()
        {
            foreach (UfcEvent ufcEvent in eventFinder.FindAllEvents())
            {
                EventWebpagesDataExtractor eventWebpagesDataExractor = new EventWebpagesDataExtractor(ufcEvent);
                eventWebpagesDataExractor.ExtractWebpagesData();
            }
        }
    }
}
