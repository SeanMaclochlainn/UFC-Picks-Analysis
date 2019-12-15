
using FightData.Domain.Entities;
using FightData.Domain.Updaters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestWebsite : TestDataLayer
    {
        [TestMethod]
        public void TestAddWebsite()
        {
            Website website = new Website(context);
            website.Id = 555;

            entityUpdater.WebsiteUpdater.Add(website);

            Assert.IsTrue(context.Websites.Count(w => w.Id == 555) == 1);
        }
    }
}
