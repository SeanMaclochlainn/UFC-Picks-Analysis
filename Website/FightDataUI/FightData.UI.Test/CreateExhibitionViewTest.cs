using FightData.Domain.Test;
using FightDataUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.UI.Test
{
    [TestClass]
    public class CreateExhibitionViewTest : TestDataLayer
    {
        [TestMethod]
        public void TestAddAllWebsiteWebpages()
        {
            CreateExhibitionView exhibitionView = new CreateExhibitionView();

            exhibitionView.LoadViewData(context);

            Assert.IsTrue(exhibitionView.Exhibition.Webpages.Count == 2);
        }

        [TestMethod]
        public void ParameterlessConstructorTest()
        {
            CreateExhibitionView exhibitionView = new CreateExhibitionView();

            Assert.IsNotNull(exhibitionView);
        }
    }
}
