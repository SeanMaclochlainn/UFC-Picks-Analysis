using FightData.Domain;

namespace FightDataProcessor
{
    public class UfcEventCollector
    {
        private UfcEventCollectingUi eventUi;
        private NewUfcEventCollector newEventCollector;
        private ExistingUfcEventCollector existingUfcEventCollector;

        public UfcEventCollector() : this(new UfcEventCollectingUi(), new FightPicksContext()) { }

        public UfcEventCollector(UfcEventCollectingUi ufcEventUi, FightPicksContext fightPicksContext)
        {
            this.eventUi = ufcEventUi;
            this.newEventCollector = new NewUfcEventCollector();
            this.existingUfcEventCollector = new ExistingUfcEventCollector();
        }

        public void ContinuouslyCollectEvents()
        {
            while (true)
                CollectEvent();
        }

        public void CollectEvent()
        {
            if (eventUi.IsNewEvent())
                newEventCollector.CollectNewEvent();
            else
                existingUfcEventCollector.UpdateExistingEvent();
        }
    }
}