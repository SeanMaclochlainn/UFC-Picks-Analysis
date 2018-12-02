using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing;
using FightDataProcessor.WebpageParsing.PicksPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestPicksPageParser : TestDataLayer
    {
        [TestMethod]
        public void TestParseAnalyst()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Webpage picksPage = entityFinder.WebpageFinder.GetPicksPages(exhibition).First();
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());
            PicksPageConfiguration picksPageConfiguration = entityFinder.PicksPageConfigurationFinder.FindConfiguration(picksPage.Website);

            List <RawAnalystPick> rawAnalystPicks = picksPageParser.ParsePicksPage(picksPageConfiguration);

            Assert.IsTrue(rawAnalystPicks.First().Analyst == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Webpage picksPage = entityFinder.WebpageFinder.GetPicksPages(exhibition).First();
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());
            PicksPageConfiguration picksPageConfiguration = entityFinder.PicksPageConfigurationFinder.FindConfiguration(picksPage.Website);

            List<RawAnalystPick> rawAnalystPicks = picksPageParser.ParsePicksPage(picksPageConfiguration).ToList();

            Assert.IsTrue(rawAnalystPicks.First().Pick == "Rockhold");
        }

        [TestMethod]
        public void TestParseParseFighterThenAnalysts()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Webpage picksPage = entityFinder.WebpageFinder.GetPicksPage(exhibition, entityFinder.WebsiteFinder.FindWebsite(WebsiteName.BloodyElbow));
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());
            PicksPageConfiguration picksPageConfiguration = entityFinder.PicksPageConfigurationFinder.FindConfiguration(picksPage.Website);

            List<RawAnalystPick> rawAnalystPicks = picksPageParser.ParsePicksPage(picksPageConfiguration);

            Assert.IsTrue(rawAnalystPicks.Single(rap => rap.Analyst == "Stephie").Pick == "Rockhold");

        }
    }
}
