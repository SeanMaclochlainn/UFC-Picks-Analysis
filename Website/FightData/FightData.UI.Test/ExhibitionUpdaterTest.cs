using FightData.Domain.Updaters;
using FightData.Domain.EntityCreation;
using FightData.Domain.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.UI.Test
{
    [TestClass]
    public class ExhibitionUpdaterTest : TestDataLayer
    {
        private ExhibitionUpdater exhibitionUpdater;

        public ExhibitionUpdaterTest()
        {
            exhibitionUpdater = new ExhibitionUpdater(context);
        }

        [TestMethod]
        public void TestAddExhibition()
        {
            int originalExhibitionCount = context.Exhibitions.Count();
            Exhibition exhibition = new Exhibition(context);
            exhibition.Name = "test exhibition";

            exhibitionUpdater.Add(exhibition);

            Assert.IsTrue(context.Exhibitions.Count() == originalExhibitionCount + 1);
        }

        [TestMethod]
        public void TestDownloadWebpageData()
        {
            ExhibitionForm exhibitionForm = new ExhibitionForm(entityGenerator.ExhibitionGenerator.GetEmptyExhibition());
            exhibitionForm.Exhibition.Webpages.Add(entityGenerator.WebpageGenerator.GetEmptyWebpage());

            exhibitionUpdater.Add(exhibitionForm, new TestClient());

            Assert.IsTrue(context.Exhibitions.Last().Webpages.First().Data == "downloadedstring");
        }

        [TestMethod]
        public void TestAddExhibitionFromForm()
        {
            ExhibitionForm exhibitionForm = new ExhibitionForm(entityGenerator.ExhibitionGenerator.GetEmptyExhibition());
            exhibitionForm.Exhibition.Webpages.Add(entityGenerator.WebpageGenerator.GetEmptyWebpage());
            int previousExhibitionCount = context.Exhibitions.Count();

            exhibitionUpdater.Add(exhibitionForm, new TestClient());

            Assert.IsTrue(context.Exhibitions.Count() == previousExhibitionCount+1);
        }

        [TestMethod]
        public void TestEditExhibitionFromForm()
        {
            exhibitionUpdater.Add(entityGenerator.ExhibitionGenerator.GetParsedExhibition());
            ExhibitionForm exhibitionForm = new ExhibitionForm(context.Exhibitions.Last());

            string update = "editexhibition";
            exhibitionUpdater.UpdateExhibition(exhibitionForm, new TestClient(update));

            Assert.IsTrue(context.Exhibitions.Last().Webpages.First().Data == update);
        }
    }
}
