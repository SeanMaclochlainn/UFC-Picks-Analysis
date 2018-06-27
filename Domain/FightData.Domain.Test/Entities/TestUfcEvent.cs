using FightData.Domain;
using FightData.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            UfcEvent ufcEvent = new UfcEvent("test event", context);

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

        private void AddEvent()
        {
            databaseDataGenerator.AddEvent();
        }
    }
}
