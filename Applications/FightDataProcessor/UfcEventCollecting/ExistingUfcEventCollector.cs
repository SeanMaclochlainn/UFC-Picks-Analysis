using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class ExistingUfcEventCollector
    {
        private UfcEventFinder ufcEventFinder;
        private UfcEventCollectingUi eventUi;
        private UfcEvent ufcEvent;
        private UfcEventWebpagesCollector ufcEventWebpagesCollector;

        public ExistingUfcEventCollector() : this(new UfcEventCollectingUi(), new UfcEventFinder(), new UfcEventWebpagesCollector())
        {

        }

        public ExistingUfcEventCollector(UfcEventCollectingUi eventUi, UfcEventFinder ufcEventFinder, UfcEventWebpagesCollector ufcEventWebpagesCollector)
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
