using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightDataUI.Models;
using FightDataUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FightDataUI.Controllers
{
    public class ExhibitionController : Controller
    {
        private FightPicksContext context;
        private WebsiteFinder websiteFinder;

        public ExhibitionController(FightPicksContext context)
        {
            this.context = context;
            websiteFinder = new WebsiteFinder(context);
        }

        public ActionResult Index()
        {
            return View(new ExhibitionVM(new ExhibitionFinder(context).FindAllExhibitions(), context));
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            Exhibition exhibition = new Exhibition(context);
            exhibition.AddAllWebsiteWebpages();
            return View(exhibition);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exhibition exhibition)
        {
            exhibition.Add();
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}