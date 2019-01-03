using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestPickFinder : TestDataLayer
    {
        private AnalystFinder analystFinder;

        public TestPickFinder()
        {
            analystFinder = new AnalystFinder(context);
        }

        [TestMethod]
        public void TestFindPick()
        {
            PickFinder pickFinder = new PickFinder(context);

            Pick pick = pickFinder.FindPick(analystFinder.FindAnalyst("Mike Bohn").Result, context.Fights.First()).Result;

            Assert.IsTrue(pick.Fighter.FullName == "luke rockhold");
        }
    }
}
