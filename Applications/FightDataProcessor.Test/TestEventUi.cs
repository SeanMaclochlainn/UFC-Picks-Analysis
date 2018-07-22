using FightDataProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FightData.Domain.Entities;
using FightData.Domain;

namespace FightDataProcessorTest
{
    [TestClass]
    public class TestEventUi
    {
        [TestMethod]
        public void TestIsNewEvent()
        {
            UfcEventCollectingUi eventUi = new UfcEventCollectingUi(new TestUI(new List<string>() { "Y" }));

            bool result = eventUi.IsNewEvent();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSelectEvent()
        {
            List<UfcEvent> ufcEvents = GetTestEventList();
            UfcEventCollectingUi eventUi = new UfcEventCollectingUi(new TestUI(new List<string>() { "1" }));

            UfcEvent selectedEvent = eventUi.SelectEvent(ufcEvents);

            Assert.IsTrue(selectedEvent.EventName == ufcEvents[0].EventName);
        }

        private List<UfcEvent> GetTestEventList()
        {
            List<UfcEvent> ufcEvents = new List<UfcEvent>()
            {
                new UfcEvent(new FightPicksContext()){ EventName = "ufc 100" },
                new UfcEvent(new FightPicksContext()){ EventName = "ufc 101"}
            };
            return ufcEvents;
        }
    }
}
