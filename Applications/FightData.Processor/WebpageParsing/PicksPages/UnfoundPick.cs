using FightData.WebpageParsing.PicksPages;

namespace FightData.Processor.WebpageParsing.PicksPages
{
    public class UnfoundPick
    {
        public UnfoundPick(RawAnalystPick rawAnalystPick, bool analystFound, bool fighterFound)
        {
            RawAnalystPick = rawAnalystPick;
            AnalystFound = analystFound;
            FighterFound = fighterFound;
        }

        public RawAnalystPick RawAnalystPick { get; private set; }
        public bool AnalystFound { get; private set; }
        public bool FighterFound { get; private set; }
    }
    
}
