using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Processor.WebpageParsing;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.Processor.WebpageParsing.PicksPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace FightData.UI.ViewModels.Reconciliation
{
    public class EntityReconciliation
    {
        private FightPicksContext context;
        public ReconciliationEntities ReconciliationEntities { get; set; }
        public SelectList FighterDropdown { get; private set; }
        public Exhibition Exhibition { get; set; }

        public void LoadData(UpdateEntitiesResult updateEntitiesResult, Exhibition exhibition)
        {
            context = exhibition.Context;
            Exhibition = exhibition;
            FighterDropdown = new SelectList(FighterFinder.GetFighters(exhibition), "Id", "FullName");
            LoadReconciliationEntities(updateEntitiesResult);
        }

        private void LoadReconciliationEntities(UpdateEntitiesResult updateEntitiesResult)
        {
            ReconciliationEntities = new ReconciliationEntities()
            {
                Odds = GetReconciliationOdds(updateEntitiesResult),
                Picks = GetReconciliationPicks(updateEntitiesResult)
            };
        }

        private List<ReconciliationOdd> GetReconciliationOdds(UpdateEntitiesResult updateEntitiesResult)
        {
            List<ReconciliationOdd> reconciliationOdds = new List<ReconciliationOdd>();
            foreach(RawFighterOdds unfoundOdd in updateEntitiesResult.UnfoundOdds)
            {
                ReconciliationOdd reconciledOdd = new ReconciliationOdd()
                {
                    FighterName = unfoundOdd.FighterName,
                    FighterOdds = unfoundOdd.Odds
                };
                reconciliationOdds.Add(reconciledOdd);
            }
            return reconciliationOdds;
        }

        private List<ReconciliationPick> GetReconciliationPicks(UpdateEntitiesResult updateEntitiesResult)
        {
            List<ReconciliationPick> reconciliationPicks = new List<ReconciliationPick>();
            foreach (UnfoundPick unfoundPick in updateEntitiesResult.UnfoundPicks.Where(up => up.AnalystFound))
            {
                ReconciliationPick reconciledPick = new ReconciliationPick()
                {
                    PickText = unfoundPick.RawAnalystPick.Pick,
                    AnalystName = unfoundPick.RawAnalystPick.Analyst
                };
                reconciledPick.CorrectAnalystId = new AnalystFinder(context).FindAnalyst(unfoundPick.RawAnalystPick.Analyst).Result.Id;
                reconciliationPicks.Add(reconciledPick);
            }
            return reconciliationPicks;
        }


    }
}
