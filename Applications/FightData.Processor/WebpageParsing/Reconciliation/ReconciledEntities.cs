using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing.Reconciliation
{
    public class ReconciledEntities
    {
        public ReconciledEntities(List<ReconciledPick> reconciledPicks, List<ReconciledOdd> reconciledOdds)
        {
            ReconciledPicks = reconciledPicks;
            ReconciledOdds = reconciledOdds;
        }

        public List<ReconciledPick> ReconciledPicks { get; private set; }
        public List<ReconciledOdd> ReconciledOdds { get; private set; }
    }
}
