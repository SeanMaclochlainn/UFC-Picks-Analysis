using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.UI.Models;
using FightData.UI.ViewModels;
using FightData.UI.ViewModels.Reconciliation;
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
        private ExhibitionFinder exhibitionFinder;
        private ExhibitionUpdater exhibitionUpdater;

        public ExhibitionController(FightPicksContext context)
        {
            this.context = context;
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
            exhibitionForm.AddWebpages(context);
            return View(exhibitionForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExhibitionForm exhibitionForm)
        {
            ExhibitionUpdater exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionUpdater.Add(exhibitionForm);
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
            ExhibitionWebpagesParser exhibitionDataExtractor = new ExhibitionWebpagesParser(context);
            RawExhibitionEntities rawExhibitionEntities = exhibitionDataExtractor.ParseAllWebpages(exhibition);
            RawEntitiesUpdater entitiesUpdater = new RawEntitiesUpdater(context);
            UpdateEntitiesResult updateEntitiesResult = entitiesUpdater.UpdateEntities(rawExhibitionEntities, exhibition);
            List<UnfoundPick> unfoundPicks = updateEntitiesResult.UnfoundPicks;
            if (unfoundPicks.Count > 0)
            {
                return LoadUnfoundPicksPage(updateEntitiesResult, exhibition);
            }
            else
                return RedirectToAction("Index");
        }

        public ViewResult LoadUnfoundPicksPage(UpdateEntitiesResult updateEntitiesResult, Exhibition exhibition)
        {
            EntityReconciliation entityReconciliation = new EntityReconciliation();
            entityReconciliation.LoadData(updateEntitiesResult, exhibition);
            return View("EntityReconciliation", entityReconciliation);
        }

        [HttpPost]
        public ActionResult EntityReconciliation(EntityReconciliation entityReconciliation)
        {
            ReconciledEntitiesUpdater reconciledEntitiesUpdater = new ReconciledEntitiesUpdater(context);
            reconciledEntitiesUpdater.AddReconciledEntities(entityReconciliation.ReconciliationEntities.GetReconciledEntities(), exhibitionFinder.FindExhibition(entityReconciliation.Exhibition.Id));
            return RedirectToAction("Index");
        }

        public ActionResult DeleteParsedData(int id)
        {
            exhibitionUpdater.DeleteParsedData(exhibitionFinder.FindExhibition(id));
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllParsedData()
        {
            exhibitionUpdater.DeleteAllParsedData();
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