using FightData.Domain.Entities;
using FightData.Domain.Updaters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Updaters
{
    [TestClass]
    public class TestExhibitionUpdater : TestDataLayer
    {
        [TestMethod]
        public void TestDeleteExhibition()
        {
            int originalExhibitionCount = context.Exhibitions.Count();
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();
            exhibition.Add();
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);

            exhibitionUpdater.Delete(exhibition);

            Assert.IsTrue(originalExhibitionCount == context.Exhibitions.Count());
        }

    }
}
