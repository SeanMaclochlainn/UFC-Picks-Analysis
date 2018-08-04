using FightData.TestData;
using FightData.TestData.EntityGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestDataRemover : TestDataLayer
    {
        private DataRemover dataRemover;
        private PickGenerator pickGenerator;

        public TestDataRemover()
        {
            dataRemover = new DataRemover(context);
            pickGenerator = new PickGenerator(context);
        }

        [TestMethod]
        public void TestRemovePick()
        {
            pickGenerator.GetPopulatedPick().Add();

            dataRemover.RemoveAllPicks();

            Assert.IsTrue(context.Picks.Count() == 0);
        }

    }
}
