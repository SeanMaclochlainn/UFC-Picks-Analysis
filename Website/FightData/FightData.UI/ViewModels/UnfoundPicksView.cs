using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Processor.WebpageParsing.PicksPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace FightData.UI.ViewModels
{
    public class UnfoundPicksView
    {
        private FightPicksContext context;
        public List<ReconciledPick> ReconciledPicks { get; private set; } = new List<ReconciledPick>();
        public SelectList Fighters { get; private set; }
        public Exhibition Exhibition { get; set; }

        public void LoadData(List<UnfoundPick> unfoundPicks, Exhibition exhibition)
        {
            context = exhibition.Context;
            Exhibition = exhibition;
            Fighters = new SelectList(FighterFinder.GetFighters(exhibition), "Id", "FullName");
            LoadPicksForReconciliation(unfoundPicks);
        }

        private void LoadPicksForReconciliation(List<UnfoundPick> unfoundPicks)
        {
            foreach (UnfoundPick unfoundPick in unfoundPicks.Where(up => up.AnalystFound))
            {
                ReconciledPick reconciledPick = new ReconciledPick(unfoundPick.RawAnalystPick);
                reconciledPick.CorrectAnalystId = new AnalystFinder(context).FindAnalyst(unfoundPick.RawAnalystPick.Analyst).Result.Id;
                reconciledPick.Cancelled = true;
                ReconciledPicks.Add(reconciledPick);
            }
        }


    }
}
