using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightDataUI.Models;
using FightDataUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace FightDataUI.Controllers
{
    public class ExhibitionController : Controller
    {
        private FightPicksContext context;
        private WebsiteFinder websiteFinder;

        public ExhibitionController()
        {
            context = new FightPicksContext();
            websiteFinder = new WebsiteFinder(context);
        }

        public ActionResult Index()
        {
            return View(new ExhibitionFinder(context).FindAllExhibitions());
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            ExhibitionData exhibitionData = new ExhibitionData();
            foreach (Website website in websiteFinder.FindAllWebsites())
                exhibitionData.Webpages.Add(new Webpage(context) { Website = website });
            return View(exhibitionData);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExhibitionData exhibitionData)
        {
            Exhibition exhibition = new Exhibition(context);
            exhibition.Name = exhibitionData.Name;
            exhibition.Webpages = exhibitionData.Webpages;
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