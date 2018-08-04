using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestUfcEvent : TestDataLayer
    {
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
            entityGenerator.UfcEventGenerator.GetPopulatedUfcEvent().Add();
            UfcEvent ufcEvent = context.UfcEvents.First();

            ufcEvent.Webpages.Add(entityGenerator.WebpageGenerator.GetEmptyWebpage());
            ufcEvent.Update();

            Assert.IsTrue(ufcEvent.Webpages.Count() == 2);
        }

        [TestMethod]
        public void TestGetFighters()
        {
            UfcEvent ufcEvent = entityGenerator.UfcEventGenerator.GetPopulatedUfcEvent();

            List<Fighter> fighters = ufcEvent.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }
    }
}
