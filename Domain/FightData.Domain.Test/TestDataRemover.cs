using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestDataRemover : TestDomain
    {
        private DataRemover dataRemover;

        public TestDataRemover()
        {
            dataRemover = new DataRemover(context);
            AddTestData();
        }

        [TestMethod]
        public void TestRemovePick()
        {
            dataRemover.RemoveAllPicks();

            Assert.IsTrue(context.Picks.Count() == 0);
        }

        private void AddTestData()
        {
            databaseDataGenerator.AddPick();
        }
    }
}
