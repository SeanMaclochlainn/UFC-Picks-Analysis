using FightDataProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FightData.Domain.Entities;
using FightData.Domain;

namespace FightDataProcessorTest
{
    [TestClass]
    public class TestExhibitionUi
    {
        [TestMethod]
        public void TestIsNewExhibition()
        {
            ExhibitionCollectingUi exhibitionUi = new ExhibitionCollectingUi(new TestUI(new List<string>() { "Y" }));

            bool result = exhibitionUi.IsNewExhibition();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSelectExhibition()
        {
            List<Exhibition> exhibitions = GetTestExhibitionList();
            ExhibitionCollectingUi exhibitionUi = new ExhibitionCollectingUi(new TestUI(new List<string>() { "1" }));

            Exhibition selectedExhibition = exhibitionUi.SelectExhibition(exhibitions);

            Assert.IsTrue(selectedExhibition.Name == exhibitions[0].Name);
        }

        private List<Exhibition> GetTestExhibitionList()
        {
            List<Exhibition> exhibitions = new List<Exhibition>()
            {
                new Exhibition(new FightPicksContext()){ Name = "ufc 100" },
                new Exhibition(new FightPicksContext()){ Name = "ufc 101"}
            };
            return exhibitions;
        }
    }
}
