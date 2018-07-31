using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestUfcEvent : TestDomain
    {
        public TestUfcEvent()
        {
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

            ufcEvent.Webpages.Add(entityDataGenerator.GetWebpage());
            ufcEvent.Update();

            Assert.IsTrue(ufcEvent.Webpages.Count() == 2);
        }

        [TestMethod]
        public void TestGetFighters()
        {
            UfcEvent ufcEvent = entityDataGenerator.GetPopulatedUfcEvent();

            List<Fighter> fighters = ufcEvent.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }

        private void AddEvent()
        {
            databaseDataGenerator.AddEvent();
        }
    }
}
