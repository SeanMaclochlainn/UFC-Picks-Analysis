using FightData.Domain.Entities;
using FightData.TestData;
using FightData.TestData.EntityGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestUfcEvent : TestDataLayer
    {
        private WebpageGenerator webpageGenerator;
        private UfcEventGenerator ufcEventGenerator;
        private TestDatabaseDataAdder databaseDataAdder;

        public TestUfcEvent()
        {
            webpageGenerator = new WebpageGenerator(context);
            ufcEventGenerator = new UfcEventGenerator(context);
            databaseDataAdder = new TestDatabaseDataAdder(context);
            AddEvent();
        }

        [TestMethod]
        public void TestAddEvent()
        {
            UfcEvent ufcEvent = new UfcEvent(context);
            ufcEvent.EventName = "test event";

            ufcEvent.Add();

            Assert.IsTrue(context.UfcEvents.Count() == 2);
        }

        [TestMethod]
        public void TestUpdateEvent()
        {
            UfcEvent ufcEvent = context.UfcEvents.First();

            ufcEvent.Webpages.Add(webpageGenerator.GetEmptyWebpage());
            ufcEvent.Update();

            Assert.IsTrue(ufcEvent.Webpages.Count() == 2);
        }

        [TestMethod]
        public void TestGetFighters()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();

            List<Fighter> fighters = ufcEvent.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }

        private void AddEvent()
        {
            databaseDataAdder.AddPopulatedEvent();
        }
    }
}
