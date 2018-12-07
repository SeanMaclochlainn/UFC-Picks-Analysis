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
        public SelectList AnalystDropdown { get; private set; }
        public Exhibition Exhibition { get; set; }

        public void LoadData(UpdateEntitiesResult updateEntitiesResult, Exhibition exhibition)
        {
            context = exhibition.Context;
            Exhibition = exhibition;
            FighterDropdown = new SelectList(FighterFinder.GetFighters(exhibition), "Id", "FullName");
            AnalystDropdown = new SelectList(new AnalystFinder(context).GetAllAnalysts(), "Id", "Name");
            LoadReconciliationEntities(updateEntitiesResult);
        }

        private static ReconciliationPick ExtractReconciliationPick(UnfoundPick unfoundPick)
        {
            return new ReconciliationPick() { PickText = unfoundPick.RawAnalystPick.Pick, AnalystName = unfoundPick.RawAnalystPick.Analyst };
        }

        private void LoadReconciliationEntities(UpdateEntitiesResult updateEntitiesResult)
        {
            ReconciliationEntities = new ReconciliationEntities()
            {
                UnfoundOdds = GetReconciliationOdds(updateEntitiesResult),
                UnfoundFighters = GetUnfoundFighterPicks(updateEntitiesResult),
                UnfoundAnalysts = GetUnfoundAnalystPicks(updateEntitiesResult)
            };
        }

        private List<ReconciliationOdd> GetReconciliationOdds(UpdateEntitiesResult updateEntitiesResult)
        {
            List<ReconciliationOdd> reconciliationOdds = new List<ReconciliationOdd>();
            foreach(RawFighterOdds unfoundOdd in updateEntitiesResult.UnfoundOdds)
            {
                ReconciliationOdd reconciliationOdd = new ReconciliationOdd()
                {
                    FighterName = unfoundOdd.FighterName,
                    FighterOdds = unfoundOdd.Odds
                };
                reconciliationOdds.Add(reconciliationOdd);
            }
            return reconciliationOdds;
        }

        private List<ReconciliationPick> GetUnfoundFighterPicks(UpdateEntitiesResult updateEntitiesResult)
        {
            List<ReconciliationPick> reconciliationPicks = new List<ReconciliationPick>();
            foreach (UnfoundPick unfoundPick in updateEntitiesResult.UnfoundPicks.Where(up => up.AnalystFound))
            {
                ReconciliationPick reconciliationPick = ExtractReconciliationPick(unfoundPick);
                reconciliationPick.CorrectAnalystId = new AnalystFinder(context).FindAnalyst(unfoundPick.RawAnalystPick.Analyst).Result.Id;
                reconciliationPicks.Add(reconciliationPick);
            }
            return reconciliationPicks;
        }

        private List<ReconciliationPick> GetUnfoundAnalystPicks(UpdateEntitiesResult updateEntitiesResult)
        {
            List<ReconciliationPick> reconciliationPicks = new List<ReconciliationPick>();
            foreach (UnfoundPick unfoundPick in updateEntitiesResult.UnfoundPicks.Where(up => up.FighterFound))
            {
                ReconciliationPick reconciliationPick = ExtractReconciliationPick(unfoundPick);
                reconciliationPick.CorrectFighterId = new FighterFinder(context).FindFighter(unfoundPick.RawAnalystPick.Pick, Exhibition).Result.Id;
                reconciliationPicks.Add(reconciliationPick);
            }
            return reconciliationPicks;
        }

    }
}
