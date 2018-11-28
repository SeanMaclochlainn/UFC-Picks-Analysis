using FightData.Processor.WebpageParsing.OddsPage;
using FightData.Processor.WebpageParsing.PicksPages;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.WebpageParsing
{
    public class UpdateEntitiesResult
    {
        public UpdateEntitiesResult(List<UnfoundPick> unfoundPicks, List<RawFighterOdds> unfoundOdds)
        {
            UnfoundPicks = unfoundPicks;
            UnfoundOdds = unfoundOdds;
        }

        public List<UnfoundPick> UnfoundPicks { get; private set; }
        public List<RawFighterOdds> UnfoundOdds { get; private set; }
    }
}
