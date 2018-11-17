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
            Exhibition exhibition = GetResultsPageExhibition();
            exhibitionUpdater.Add(exhibition);
            ExhibitionDataExtractor exhibitionDataExtractor = new ExhibitionDataExtractor(exhibition);
            int existingFights = context.Fights.Count();

            exhibitionDataExtractor.ExtractResultsPageData();

            Assert.IsTrue(context.Fights.Count() == existingFights + 2);
        }

        private Exhibition GetResultsPageExhibition()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetEmptyExhibition();
            exhibition.Webpages.Add(entityGenerator.WebpageGenerator.GetPopulatedResultsPage());
            return exhibition;
        }
    }
}
