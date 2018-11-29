namespace FightData.Processor.WebpageParsing.Reconciliation
{
    public class ReconciledOdd
    {
        public ReconciledOdd(int correctFighterId, string rawOdds)
        {
            CorrectFighterId = correctFighterId;
            RawOdds = rawOdds;
        }

        public string RawOdds { get; private set; }
        public int CorrectFighterId { get; private set; }
    }
}
