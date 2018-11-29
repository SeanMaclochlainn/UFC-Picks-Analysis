using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FightFinder : DataFinder
    {
        private FighterFinder fighterFinder;

        public FightFinder(FightPicksContext context) : base(context)
        {
            fighterFinder = new FighterFinder(context);
        }

        public FinderResult<Fight> FindFight(Fighter fighterFromFight, Exhibition exhibition)
        {
            foreach (Fight fight in exhibition.Fights.ToList())
            {
                if (FighterFinder.FindFighter(fighterFinder.GetFighters(fight), fighterFromFight.Id).IsFound())
                    return new FinderResult<Fight>(fight);
            }
            return new FinderResult<Fight>(null);
        }
    }
}
