using FightData.WebpageParsing.PicksPages;

namespace FightData.Processor.WebpageParsing.PicksPages
{
    public class ReconciledPick
    {
        public ReconciledPick() { }

        public ReconciledPick(RawAnalystPick rawAnalystPick)
        {
            RawAnalystPick = rawAnalystPick;
        }

        public RawAnalystPick RawAnalystPick { get; set; }
        public int CorrectFighterPickId { get; set; }
        public int CorrectAnalystId { get; set; }
        public bool Cancelled { get; set; }
    }
}
