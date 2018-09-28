
using FightData.Domain.Entities;
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
            website.WebsiteName = WebsiteName.Wikipedia;

            website.Add();

            Assert.IsTrue(context.Websites.Count() == 1);
        }
    }
}
