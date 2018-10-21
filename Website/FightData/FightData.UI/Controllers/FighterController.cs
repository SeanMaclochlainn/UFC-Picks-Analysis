using FightData.Domain;
using FightData.Domain.Finders;
using Microsoft.AspNetCore.Mvc;

namespace FightData.UI.Controllers
{
    public class FighterController : Controller
    {
        private FightPicksContext context;
        private FighterFinder fighterFinder;
        private FighterUpdater fighterUpdater;

        public FighterController(FightPicksContext context) 
        {
            this.context = context;
            fighterFinder = new FighterFinder(context);
            fighterUpdater = new FighterUpdater(context);
        }

        public ActionResult Index()
        {
            return View(fighterFinder.GetAllFighters());
        }

        public ActionResult DeleteAll()
        {
            fighterUpdater.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}