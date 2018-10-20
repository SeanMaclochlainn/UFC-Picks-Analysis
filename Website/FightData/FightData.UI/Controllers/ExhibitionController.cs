using FightData.Domain;
using FightData.Domain.Updaters;
using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
using FightData.Domain.Finders;
using FightData.UI.Models;
using FightData.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FightDataProcessor.WebpageParsing;

namespace FightDataUI.Controllers
{
    public class ExhibitionController : Controller
    {
        private FightPicksContext context;
        private WebsiteFinder websiteFinder;
        private ExhibitionFinder exhibitionFinder;
        private ExhibitionUpdater exhibitionUpdater;

        public ExhibitionController(FightPicksContext context)
        {
            this.context = context;
            websiteFinder = new WebsiteFinder(context);
            exhibitionFinder = new ExhibitionFinder(context);
            exhibitionUpdater = new ExhibitionUpdater(context);
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
            ExhibitionForm exhibitionForm = new ExhibitionForm();
            exhibitionForm.LoadDataForInput(context, new Exhibition());
            return View(exhibitionForm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExhibitionForm exhibitionForm)
        {
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionUpdater.AddExhibition(exhibitionForm, new ConnectedClient());
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            ExhibitionForm exhibitionForm = new ExhibitionForm(exhibitionFinder.FindExhibition(id));
            return View(exhibitionForm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExhibitionForm exhibitionForm)
        {
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionUpdater.UpdateExhibition(exhibitionForm, new ConnectedClient());
            return RedirectToAction("Index");
        }

        public ActionResult Process(int id)
        {
            ExhibitionDataExtractor exhibitionDataExtractor = new ExhibitionDataExtractor(exhibitionFinder.FindExhibition(id));
            exhibitionDataExtractor.ExtractAllWebpages();
            return RedirectToAction("Index");
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
                exhibitionUpdater.Delete(exhibition);
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