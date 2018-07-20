using FightData.Domain.Finders;
using FightData.Domain.Entities;

namespace FightDataProcessor
{
    public class ExistingUfcEventCollector
    {
        private EventFinder ufcEventFinder;
        private UfcEventCollectingUi eventUi;
        private UfcEvent ufcEvent;
        private UfcEventWebpagesCollector ufcEventWebpagesCollector;

        public ExistingUfcEventCollector() : this(new UfcEventCollectingUi(), new EventFinder(), new UfcEventWebpagesCollector())
        {

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
