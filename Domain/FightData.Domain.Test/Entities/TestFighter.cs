using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFighter : TestDomain
    {
        [TestMethod]
        public void TestAddFighter()
        {
            Fighter fighter = new Fighter("fname lname", context);

            fighter.Add();

            Assert.IsTrue(context.Fighters.Count() == 1);
        }
    }
}
