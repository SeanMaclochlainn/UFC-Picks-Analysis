using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestPickFinder : TestDataLayer
    {
        [TestMethod]
        public void TestFindPick()
        {
            PickFinder pickFinder = new PickFinder(context);
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();

            Pick pick = pickFinder.FindPick(entityGenerator.AnalystGenerator.GetPopulatedAnalyst(), exhibition.Fights.First()).Result;

            Assert.IsTrue(pick.Fighter.FullName == "Luke Rockhold");
        }
    }
}
