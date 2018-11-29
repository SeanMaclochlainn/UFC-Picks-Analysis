using FightData.Processor.WebpageParsing.Reconciliation;
using System.Collections.Generic;
using System.Linq;

namespace FightData.UI.ViewModels.Reconciliation
{
    public class ReconciliationEntities
    {
        public ReconciliationEntities() { }

        public List<ReconciliationPick> Picks { get; set; }
        public List<ReconciliationOdd> Odds { get; set; }

        public ReconciledEntities GetReconciledEntities()
        {
            return new ReconciledEntities(GetReconciledPicks(), GetReconciledOdds());
        }

        private List<ReconciledOdd> GetReconciledOdds()
        {
            List<ReconciledOdd> reconciledOdds = new List<ReconciledOdd>();
            foreach (ReconciliationOdd reconciliationOdd in Odds.Where(o => o.CorrectFighterId != 0))
                reconciledOdds.Add(new ReconciledOdd(reconciliationOdd.CorrectFighterId, reconciliationOdd.FighterOdds));
            return reconciledOdds;
        }

        private List<ReconciledPick> GetReconciledPicks()
        {
            List<ReconciledPick> reconciledPicks = new List<ReconciledPick>();
            foreach (ReconciliationPick reconciliationPick in Picks.Where(o => o.CorrectFighterId != 0))
                reconciledPicks.Add(new ReconciledPick(reconciliationPick.CorrectFighterId, reconciliationPick.CorrectAnalystId));
            return reconciledPicks;
        }

    }
}
