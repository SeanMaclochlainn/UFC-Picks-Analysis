using FightData.Processor.WebpageParsing.Reconciliation;
using System.Collections.Generic;
using System.Linq;

namespace FightData.UI.ViewModels.Reconciliation
{
    public class ReconciliationEntities
    {
        public ReconciliationEntities() { }

        public List<ReconciliationPick> UnfoundFighters { get; set; } = new List<ReconciliationPick>();
        public List<ReconciliationPick> UnfoundAnalysts { get; set; } = new List<ReconciliationPick>();
        public List<ReconciliationOdd> UnfoundOdds { get; set; } = new List<ReconciliationOdd>();

        public ReconciledEntities GetReconciledEntities()
        {
            return new ReconciledEntities(GetReconciledPicks(), GetReconciledOdds());
        }

        private static List<ReconciledPick> ExtractReconciledPicks(List<ReconciliationPick> reconciliationPicks)
        {
            List<ReconciledPick> reconciledPicks = new List<ReconciledPick>();
            foreach (ReconciliationPick reconciliationPick in reconciliationPicks)
                reconciledPicks.Add(new ReconciledPick(reconciliationPick.CorrectFighterId, reconciliationPick.CorrectAnalystId));
            return reconciledPicks;
        }

        private List<ReconciledOdd> GetReconciledOdds()
        {
            List<ReconciledOdd> reconciledOdds = new List<ReconciledOdd>();
            foreach (ReconciliationOdd reconciliationOdd in UnfoundOdds.Where(ua => ua.CorrectFighterId != 0))
                reconciledOdds.Add(new ReconciledOdd(reconciliationOdd.CorrectFighterId, reconciliationOdd.FighterOdds));
            return reconciledOdds;
        }

        private List<ReconciledPick> GetReconciledPicks()
        {
            List<ReconciliationPick> reconciliationPicks = new List<ReconciliationPick>();
            reconciliationPicks.AddRange(GetSelectedFighters());
            reconciliationPicks.AddRange(GetSelectedAnalysts());
            return ExtractReconciledPicks(reconciliationPicks);
        }

        private List<ReconciliationPick> GetSelectedFighters()
        {
            return UnfoundFighters.Where(uf => uf.CorrectFighterId != 0).ToList();
        }

        private List<ReconciliationPick> GetSelectedAnalysts()
        {
            return UnfoundAnalysts.Where(ua => ua.CorrectAnalystId != 0).ToList();
        }
    }
}
