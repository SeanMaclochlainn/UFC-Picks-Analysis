using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.UI.ViewModels.ExhibitionIndex;
using FightData.UI.ViewModels.Reconciliation;
using FightDataProcessor.WebpageParsing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FightDataUI.Controllers
{
    public class ExhibitionController : Controller
    {
        private FightPicksContext context;
        private EntityFinder entityFinder;
        private ExhibitionUpdater exhibitionUpdater;
        private WebpageUpdater webpageUpdater;
        private ExhibitionWebpagesParser exhibitionWebpagesParser;
        private RawEntitiesUpdater rawEntitiesUpdater;

        public ExhibitionController(FightPicksContext context)
        {
            this.context = context;
            entityFinder = new EntityFinder(context);
            exhibitionUpdater = new ExhibitionUpdater(context);
            webpageUpdater = new WebpageUpdater(context);
            exhibitionWebpagesParser = new ExhibitionWebpagesParser(context);
            rawEntitiesUpdater = new RawEntitiesUpdater(context);
        }

        public ActionResult Index()
        {
            ExhibitionGrid exhibitionGrid = new ExhibitionGrid();
            exhibitionGrid.LoadViewData(context);
            return View(exhibitionGrid);
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
            ExhibitionForm exhibitionForm = new ExhibitionForm(entityFinder.ExhibitionFinder.FindExhibition(id));
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
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition(id);
            RawExhibitionEntities rawExhibitionEntities = exhibitionWebpagesParser.ParseAllWebpages(exhibition);
            webpageUpdater.DeleteDownloadedData(exhibition);
            UpdateEntitiesResult updateEntitiesResult = rawEntitiesUpdater.UpdateEntities(rawExhibitionEntities, exhibition);
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
            reconciledEntitiesUpdater.AddReconciledEntities(entityReconciliation.ReconciliationEntities.GetReconciledEntities(), entityFinder.ExhibitionFinder.FindExhibition(entityReconciliation.Exhibition.Id));
            return RedirectToAction("Index");
        }

        public ActionResult DeleteParsedData(int id)
        {
            exhibitionUpdater.DeleteParsedData(entityFinder.ExhibitionFinder.FindExhibition(id));
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllParsedData()
        {
            exhibitionUpdater.DeleteAllParsedData();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(entityFinder.ExhibitionFinder.FindExhibition(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Exhibition exhibition)
        {
            exhibition = entityFinder.ExhibitionFinder.FindExhibition(exhibition.Id);
            exhibitionUpdater.Delete(exhibition);
            return RedirectToAction("Index");
        }

        public ActionResult RedownloadData(int id)
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition(id);
            exhibitionUpdater.UpdateExhibition(new ExhibitionForm(exhibition), new ConnectedClient());
            return RedirectToAction("Index");
        }
    }
}