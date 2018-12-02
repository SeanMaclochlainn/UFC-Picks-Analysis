using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestWebpageFinder : TestDataLayer
    {
        private WebpageFinder webpageFinder;
        private WebsiteFinder websiteFinder;

        public TestWebpageFinder()
        {
            webpageFinder = new WebpageFinder(context);
            websiteFinder = new WebsiteFinder(context);
        }

        [TestMethod]
        public void TestGetWebpage()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");

            Webpage webpage = webpageFinder.GetWebpage(exhibition, websiteFinder.FindWebsite(WebsiteName.Wikipedia));

            Assert.IsTrue(webpage.Website.WebsiteName == WebsiteName.Wikipedia);
        }
    }
}
