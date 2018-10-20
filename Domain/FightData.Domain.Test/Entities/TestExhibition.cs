using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestExhibition : TestDataLayer
    {
        [TestMethod]
        public void TestAddExhibition()
        {
            Exhibition exhibition = new Exhibition(context);
            exhibition.Name = "test exhibition";

            exhibition.Add();

            Assert.IsTrue(context.Exhibitions.Count() == 1);
        }

        [TestMethod]
        public void TestUpdateExhibition()
        {
            entityGenerator.ExhibitionGenerator.GetParsedExhibition().Add();
            Exhibition exhibition = context.Exhibitions.First();

            exhibition.Webpages.Add(entityGenerator.WebpageGenerator.GetEmptyWebpage());
            exhibition.Update();

            Assert.IsTrue(exhibition.Webpages.Count() == 2);
        }

        [TestMethod]
        public void TestGetFighters()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();

            List<Fighter> fighters = exhibition.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }

        [TestMethod]
        public void TestGetWebsiteUrl()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();

            string url = exhibition.GetWebsiteUrl(WebsiteName.Wikipedia);

            Assert.IsTrue(url == entityGenerator.WebpageGenerator.GetPopulatedResultsPage().Url);
        }

        
    }
}
