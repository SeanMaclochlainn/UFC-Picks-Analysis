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

        public TestUfcEvent()
        {
            webpageGenerator = new WebpageGenerator(context);
            ufcEventGenerator = new UfcEventGenerator(context);
        }

        [TestMethod]
        public void TestAddEvent()
        {
            UfcEvent ufcEvent = new UfcEvent(context);
            ufcEvent.EventName = "test event";

            ufcEvent.Add();

            Assert.IsTrue(context.UfcEvents.Count() == 1);
        }

        [TestMethod]
        public void TestUpdateEvent()
        {
            ufcEventGenerator.GetPopulatedUfcEvent().Add();
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
    }
}
