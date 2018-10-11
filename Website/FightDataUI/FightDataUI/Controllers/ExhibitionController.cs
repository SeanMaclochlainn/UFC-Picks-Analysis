using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
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
            CreateExhibition createExhibition = new CreateExhibition();
            createExhibition.LoadViewData(context);
            return View(createExhibition);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateExhibition createExhibition)
        {
            createExhibition.ProcessViewData(context, new ConnectedClient());
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