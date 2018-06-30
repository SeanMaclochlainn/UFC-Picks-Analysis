using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestEventDataUpdater : TestDomain
    {
        private EventDataUpdater eventDataUpdater;

        public TestEventDataUpdater()
        {
            eventDataUpdater = new EventDataUpdater(entityDataGenerator.GetUfcEvent(), context);
        }

        [TestMethod]
        public void TestAddUnknownFighters()
        {
            string winner = "winner fighter";
            string loser = "loser fighter";

            eventDataUpdater.AddFightData(winner, loser);

            Assert.IsTrue(GetFighters()[0].FullName == winner && GetFighters()[1].FullName == loser);
        }

        private List<Fighter> GetFighters()
        {
            return context.Fighters.ToList();
        }

    }
}
