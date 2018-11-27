using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing.OddsPage
{
    public class OddsEvaluatorResult
    {
        public OddsEvaluatorResult(List<Odd> odds, List<RawFighterOdds> unfoundOdds)
        {
            Odds = odds;
            UnfoundOdds = unfoundOdds;
        }

        public List<Odd> Odds { get; private set; }
        public List<RawFighterOdds> UnfoundOdds { get; private set; }
    }
}
