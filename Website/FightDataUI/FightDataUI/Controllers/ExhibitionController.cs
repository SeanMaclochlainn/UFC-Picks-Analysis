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
        private ExhibitionFinder exhibitionFinder;

        public ExhibitionController(FightPicksContext context)
        {
            this.context = context;
            websiteFinder = new WebsiteFinder(context);
            exhibitionFinder = new ExhibitionFinder(context);
        }

        public ActionResult Index()
        {
            ExhibitionIndexView exhibitionIndexView = new ExhibitionIndexView();
            exhibitionIndexView.LoadViewData(context);
            return View(exhibitionIndexView);
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            CreateExhibitionView createExhibitionView = new CreateExhibitionView();
            createExhibitionView.LoadViewData(context);
            return View(createExhibitionView);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateExhibitionView createExhibitionView)
        {
            Exhibition exhibition = new Exhibition(context, createExhibitionView.Exhibition.Name, createExhibitionView.Exhibition.Webpages);
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
            return View(exhibitionFinder.FindExhibition(id));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Exhibition exhibition)
        {
            try
            {
                exhibition = exhibitionFinder.FindExhibition(exhibition.Id);
                exhibition.Delete();
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