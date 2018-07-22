using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.WebpageParsing
{
    public class EventsDataExtractor
    {
        private EventFinder eventFinder;
        private FightPicksContext context;

        public EventsDataExtractor(FightPicksContext context)
        {
            eventFinder = new EventFinder(context);
            this.context = context;
        }

        public void ParseAllEvents()
        {
            foreach (UfcEvent ufcEvent in eventFinder.GetAllEvents())
            {
                EventWebpagesDataExtractor eventWebpagesDataExractor = new EventWebpagesDataExtractor(ufcEvent, context);
                eventWebpagesDataExractor.ParseAllWebpages();
            }
        }
    }
}
