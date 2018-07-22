using FightData.Domain;
using FightData.Domain.Entities;

namespace FightDataProcessor
{
    public class NewUfcEventCollector
    {
        private UfcEventCollectingUi eventUi;
        private UfcEvent ufcEvent;
        private UfcEventWebpagesCollector ufcEventWebpagesCollector;

        public NewUfcEventCollector() : this(new UfcEventCollectingUi())
        {

        }

        public NewUfcEventCollector(UfcEventCollectingUi ufcEventUi)
        {
            this.eventUi = ufcEventUi;
            this.ufcEvent = new UfcEvent(new FightPicksContext());
            this.ufcEventWebpagesCollector = new UfcEventWebpagesCollector();
        }

        public void CollectNewEvent()
        {
            InputName();
            ufcEvent.Webpages = ufcEventWebpagesCollector.CollectEventWebpages();
            ufcEvent.Add();
        }

        private void InputName()
        {
            ufcEvent.EventName = eventUi.GetEventName();
        }
    }
}
