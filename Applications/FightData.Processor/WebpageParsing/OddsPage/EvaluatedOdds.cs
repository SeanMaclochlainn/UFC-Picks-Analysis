using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing.OddsPage
{
    public class EvaluatedOdds
    {
        public EvaluatedOdds(List<Odd> validOdds, List<RawFighterOdds> unfoundOdds)
        {
            ValidOdds = validOdds;
            UnfoundOdds = unfoundOdds;
        }

        public List<Odd> ValidOdds { get; private set; }
        public List<RawFighterOdds> UnfoundOdds { get; private set; }
    }
}
