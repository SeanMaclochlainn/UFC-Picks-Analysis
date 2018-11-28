using FightData.Domain;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.WebpageParsing.PicksPages;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing
{
    public class RawExhibitionEntities
    {
        public RawExhibitionEntities(List<RawFightResult> rawFightResults, List<RawAnalystPick> rawAnalystPicks, List<RawFighterOdds> rawFighterOdds)
        {
            RawFightResults = rawFightResults;
            RawAnalystPicks = rawAnalystPicks;
            RawFighterOdds = rawFighterOdds;
        }

        public List<RawFightResult> RawFightResults { get; private set; }
        public List<RawAnalystPick> RawAnalystPicks { get; private set; }
        public List<RawFighterOdds> RawFighterOdds { get; private set; }
    }
}
