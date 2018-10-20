using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using System.Linq;
using FightDataProcessor.WebpageParsing;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        [TestMethod]
        public void TestExtractResults()
        {
            Exhibition exhibition = GetResultsPageExhibition();
            exhibition.Add();
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
