using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Updaters
{
    [TestClass]
    public class TestExhibitionUpdater : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;

        public TestExhibitionUpdater()
        {
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestDeleteExhibition()
        {
            int originalExhibitionCount = context.Exhibitions.Count();
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionUpdater.Delete(exhibition);

            Assert.IsTrue(originalExhibitionCount - 1 == context.Exhibitions.Count());
        }

        [TestMethod]
        public void TestDeleteParsedDataFromExhibition()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionUpdater.DeleteParsedData(exhibition);

            Assert.IsTrue(exhibition.Fights.Count() == 0);
        }

        [TestMethod]
        public void TestDeleteAllParsedData()
        {
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);

            exhibitionUpdater.DeleteAllParsedData();

            Assert.IsTrue(context.Fighters.Count() == 0);
        }
    }
}
