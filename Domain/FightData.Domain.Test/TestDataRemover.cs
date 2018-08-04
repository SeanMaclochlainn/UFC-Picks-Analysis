using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestDataRemover : TestDataLayer
    {
        private DataRemover dataRemover;

        public TestDataRemover()
        {
            dataRemover = new DataRemover(context);
        }

        [TestMethod]
        public void TestRemovePick()
        {
            entityGenerator.PickGenerator.GetPopulatedPick().Add();

            dataRemover.RemoveAllPicks();

            Assert.IsTrue(context.Picks.Count() == 0);
        }

    }
}
