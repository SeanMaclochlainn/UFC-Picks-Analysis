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
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            ExhibitionForm exhibitionForm = new ExhibitionForm(exhibition);
            exhibitionForm.Exhibition.Webpages.Add(exhibition.Webpages.First());

            exhibitionUpdater.Add(exhibitionForm, new TestClient());

            Assert.IsTrue(context.Exhibitions.Last().Webpages.First().Data == "downloadedstring");
        }

        [TestMethod]
        public void TestAddExhibitionFromForm()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            ExhibitionForm exhibitionForm = new ExhibitionForm(exhibition);
            exhibitionForm.Exhibition.Webpages.Add(exhibition.Webpages.First());
            int previousExhibitionCount = context.Exhibitions.Count();

            exhibitionUpdater.Add(exhibitionForm, new TestClient());

            Assert.IsTrue(context.Exhibitions.Count() == previousExhibitionCount + 1);
        }

        [TestMethod]
        public void TestEditExhibitionFromForm()
        {
            ExhibitionForm exhibitionForm = new ExhibitionForm(entityFinder.ExhibitionFinder.FindExhibition("FN 55"));

            string update = "editexhibition";
            exhibitionUpdater.UpdateExhibition(exhibitionForm, new TestClient(update));

            Assert.IsTrue(entityFinder.ExhibitionFinder.FindExhibition("FN 55").Webpages.First().Data == update);
        }
    }
}
