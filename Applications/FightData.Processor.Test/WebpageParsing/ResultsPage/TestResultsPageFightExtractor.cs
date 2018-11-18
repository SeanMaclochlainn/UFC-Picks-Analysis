using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using System.Linq;
using FightDataProcessor.WebpageParsing;
using FightData.Domain.Updaters;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        private ExhibitionUpdater exhibitionUpdater;

        public TestResultsPageFightExtractor()
        {
            exhibitionUpdater = new ExhibitionUpdater(context);
        }

        [TestMethod]
        public void TestExtractResults()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");
            int existingFights = context.Fights.Count();

            ExhibitionDataExtractor exhibitionDataExtractor = new ExhibitionDataExtractor(exhibition);
            exhibitionDataExtractor.ExtractAllWebpages();

            Assert.IsTrue(context.Fights.Count() == existingFights + 2);
        }
    }
}
