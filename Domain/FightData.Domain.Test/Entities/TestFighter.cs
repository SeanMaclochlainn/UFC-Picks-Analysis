using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFighter : TestDataLayer
    {
        [TestMethod]
        public void TestAddFighter()
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames("fname lname");

            fighter.Add();

            Assert.IsTrue(context.Fighters.Count() == 1);
        }
    }
}
