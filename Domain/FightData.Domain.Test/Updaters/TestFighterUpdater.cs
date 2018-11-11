using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Updaters
{
    [TestClass]
    public class TestFighterUpdater : TestDataLayer
    {
        [TestMethod]
        public void TestDeleteAll()
        {
            FighterUpdater fighterUpdater = new FighterUpdater(context);

            fighterUpdater.DeleteAll();

            Assert.IsTrue(context.Fighters.Count() == 0);
        }
    }
}
