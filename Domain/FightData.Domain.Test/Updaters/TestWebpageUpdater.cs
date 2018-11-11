using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.Domain.Test.Updaters
{
    [TestClass]
    public class TestWebpageUpdater : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;

        public TestWebpageUpdater()
        {
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestMarkWebpagesUnparsed()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            WebpageUpdater webpageUpdater = new WebpageUpdater(context);
            webpageUpdater.MarkAsUnparsed(exhibition.Webpages);

            Assert.IsTrue(exhibition.Webpages.TrueForAll(w => w.Parsed == false));
        }
    }
}
