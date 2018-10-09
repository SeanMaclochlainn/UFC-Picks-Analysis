using FightData.Domain.Test;
using FightDataUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.UI.Test
{
    [TestClass]
    public class CreateExhibitionTest : TestDataLayer
    {
        [TestMethod]
        public void TestAddAllWebsiteWebpages()
        {
            CreateExhibition createExhibition = new CreateExhibition();

            createExhibition.LoadViewData(context);

            Assert.IsTrue(createExhibition.Exhibition.Webpages.Count == 2);
        }

        [TestMethod]
        public void ParameterlessConstructorTest()
        {
            CreateExhibition createExhibition = new CreateExhibition();

            Assert.IsNotNull(createExhibition);
        }

        [TestMethod]
        public void TestDataDownload()
        {
            CreateExhibition createExhibition = new CreateExhibition(entityGenerator.ExhibitionGenerator.GetEmptyExhibition());
            createExhibition.Exhibition.Webpages.Add(entityGenerator.WebpageGenerator.GetEmptyWebpage());

            createExhibition.ProcessViewData(context, new TestClient());

            Assert.IsTrue(context.Exhibitions.Last().Webpages.First().Data == "downloadedstring");
        }

    }
}
