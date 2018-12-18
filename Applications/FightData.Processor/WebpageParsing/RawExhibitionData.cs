using FightData.Processor.WebpageParsing.OddsPage;
using FightData.WebpageParsing.PicksPages;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing
{
    public class RawExhibitionData
    {
        public RawExhibitionData(RawResultsPageData rawResultsPageData, List<RawAnalystPick> rawAnalystPicks, List<RawFighterOdds> rawFighterOdds)
        {
            ResultsPageData = rawResultsPageData;
            RawAnalystPicks = rawAnalystPicks;
            RawFighterOdds = rawFighterOdds;
        }

        public RawResultsPageData ResultsPageData { get; private set; }
        public List<RawAnalystPick> RawAnalystPicks { get; private set; }
        public List<RawFighterOdds> RawFighterOdds { get; private set; }
    }
}
