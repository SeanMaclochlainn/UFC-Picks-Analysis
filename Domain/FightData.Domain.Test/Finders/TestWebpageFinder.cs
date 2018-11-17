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
        public void TestGetWebsiteUrl()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();

            string url = webpageFinder.GetWebpage(exhibition, websiteFinder.GetWebsite(WebsiteName.Wikipedia)).Url;

            Assert.IsTrue(url == entityGenerator.WebpageGenerator.GetPopulatedResultsPage().Url);
        }
    }
}
