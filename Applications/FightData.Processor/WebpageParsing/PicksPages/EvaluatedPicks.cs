using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing.PicksPages
{
    public class EvaluatedPicks
    {
        public EvaluatedPicks(List<UnfoundPick> unfoundPicks, List<Pick> validPicks)
        {
            UnfoundPicks = unfoundPicks;
            ValidPicks = validPicks;
        }

        public List<UnfoundPick> UnfoundPicks { get; private set; } = new List<UnfoundPick>();
        public List<Pick> ValidPicks { get; private set; } = new List<Pick>();
    }
}
