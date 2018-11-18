using FightData.Domain.EntityCreation;
using FightData.Domain.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.UI.Test
{
    [TestClass]
    public class ExhibitionFormTest : TestDataLayer
    {
        [TestMethod]
        public void TestAddAllWebsiteWebpages()
        {
            ExhibitionForm exhibitionForm = new ExhibitionForm();

            exhibitionForm.AddWebpages(context);

            Assert.IsTrue(exhibitionForm.Exhibition.Webpages.Count == 3);
        }

        [TestMethod]
        public void ParameterlessConstructorTest()
        {
            ExhibitionForm exhibitionForm = new ExhibitionForm();

            Assert.IsNotNull(exhibitionForm.Exhibition);
        }

    }
}
