using FightData.Domain.Finders;
using FightData.Domain.Entities;
using FightData.Domain;

namespace FightDataProcessor
{
    public class ExistingUfcEventCollector
    {
        private EventFinder ufcEventFinder;
        private UfcEventCollectingUi eventUi;
        private UfcEvent ufcEvent;
        private UfcEventWebpagesCollector ufcEventWebpagesCollector;
        private FightPicksContext context;

        public ExistingUfcEventCollector(FightPicksContext context) : this(new UfcEventCollectingUi(), new EventFinder(context), new UfcEventWebpagesCollector())
        {
            this.context = context;
        }

        public ExistingUfcEventCollector(UfcEventCollectingUi eventUi, EventFinder ufcEventFinder, UfcEventWebpagesCollector ufcEventWebpagesCollector)
        {
            this.eventUi = eventUi;
            this.ufcEventFinder = ufcEventFinder;
            this.ufcEventWebpagesCollector = ufcEventWebpagesCollector;
        }

        public void UpdateExistingEvent()
        {
            SelectExistingEvent();
            ufcEvent.Webpages = ufcEventWebpagesCollector.CollectEventWebpages();
            ufcEvent.Update();
        }

        private void SelectExistingEvent()
        {
            ufcEvent = eventUi.SelectEvent(ufcEventFinder.GetAllEvents());
        }
    }
}
