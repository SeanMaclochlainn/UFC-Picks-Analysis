using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.Domain.Updaters;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestExhibitionDataExtractor : TestDataLayer
    {
        WebpageFinder webpageFinder;

        public TestExhibitionDataExtractor()
        {
            webpageFinder = new WebpageFinder(context);
        }

        [TestMethod]
        public void TestSkipParsedPage()
        {
            int originalNoFights = context.Fights.Count();
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");

            new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Assert.IsTrue(originalNoFights == context.Fights.Count());

        }

        [TestMethod]
        public void TestWebpageIsMarkedAsParsed()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Assert.IsTrue(webpageFinder.GetResultsPage(exhibition).Parsed == true);
        }

        [TestMethod]
        public void TestExtractOddsPage()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");
            int originalOddsCount = context.Odds.Count();

            new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Assert.IsTrue(context.Odds.Count() == originalOddsCount + 1);
        }
    }
}
