using FightData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestDataRemover : TestDataLayer
    {
        private DataRemover dataRemover;
        private TestDatabaseDataAdder databaseDataAdder;

        public TestDataRemover()
        {
            databaseDataAdder = new TestDatabaseDataAdder(context);
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
            databaseDataAdder.AddPopulatedPick();
        }
    }
}
