using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.UI.Models;
using FightData.UI.ViewModels;
using FightDataProcessor.WebpageParsing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

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

        public ActionResult ExtractWebpages(int id)
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition(id);
            ExhibitionDataExtractor exhibitionDataExtractor = new ExhibitionDataExtractor(exhibition);
            exhibitionDataExtractor.ExtractResultsPageData();
            exhibitionDataExtractor.ExtractPicksPagesData();
            List<UnfoundPick> unfoundPicks = exhibitionDataExtractor.UnfoundPicks;
            if (unfoundPicks.Count > 0)
            {
                return LoadUnfoundPicksPage(unfoundPicks, exhibition);
            }
            else
                return RedirectToAction("Index");
        }

        public ViewResult LoadUnfoundPicksPage(List<UnfoundPick> unfoundPicks, Exhibition exhibition)
        {
            UnfoundPicksView unfoundPicksView = new UnfoundPicksView();
            unfoundPicksView.LoadData(unfoundPicks, exhibition);
            return View("UnfoundPicks", unfoundPicksView);
        }

        [HttpPost]
        public ActionResult UnfoundPicks(UnfoundPicksView unfoundPicksView)
        {
            ReconciledPicksAdder reconciledPicksAdder = new ReconciledPicksAdder(context);
            reconciledPicksAdder.AddReconciledPicks(unfoundPicksView.ReconciledPicks, exhibitionFinder.FindExhibition(unfoundPicksView.Exhibition.Id));
            return RedirectToAction("Index");
        }

        public ActionResult DeleteParsedData(int id)
        {
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionUpdater.DeleteParsedData(exhibitionFinder.FindExhibition(id));
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
            exhibition = exhibitionFinder.FindExhibition(exhibition.Id);
            exhibitionUpdater.Delete(exhibition);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}