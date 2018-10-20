using FightData.Domain;
using FightData.Domain.Views;
using Microsoft.AspNetCore.Mvc;

namespace FightDataUI.Controllers
{
    public class PicksController : Controller
    {
        private FightPicksContext context;

        public PicksController(FightPicksContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            PickView pickView = new PickView(context);
            pickView.LoadData();
            return View(pickView);
        }

    }
}