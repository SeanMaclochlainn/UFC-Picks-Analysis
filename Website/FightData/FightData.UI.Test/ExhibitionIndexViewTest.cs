using FightData.Domain.Test;
using FightData.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.UI.Test
{
    [TestClass]
    public class ExhibitionIndexViewTest : TestDataLayer
    {
        [TestMethod]
        public void TestLoadData()
        {
            ExhibitionIndexView exhibitionIndexView = new ExhibitionIndexView();

            exhibitionIndexView.LoadViewData(context);

            Assert.IsTrue(exhibitionIndexView.Websites.Count() == 4);
        }

        
    }
}
