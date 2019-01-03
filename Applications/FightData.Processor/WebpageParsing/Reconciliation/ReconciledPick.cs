namespace FightData.Processor.WebpageParsing.Reconciliation
{
    public class ReconciledPick
    {
        public ReconciledPick(int correctFighterId, int correctAnalystId)
        {
            CorrectFighterId = correctFighterId;
            CorrectAnalystId = correctAnalystId;
        }

        public int CorrectFighterId { get; private set; }
        public int CorrectAnalystId { get; private set; }
    }
}
