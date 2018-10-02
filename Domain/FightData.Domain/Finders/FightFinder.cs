using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FightFinder : DataFinder
    {
        private Exhibition exhibition;
        private FighterFinder fighterFinder;

        private FightFinder(Exhibition exhibition, FightPicksContext context) : base(context)
        {
            this.exhibition = exhibition;
            fighterFinder = new FighterFinder(context);
        }

        public static FightFinder WithinExhibition(Exhibition exhibition, FightPicksContext context)
        {
            return new FightFinder(exhibition, context);
        }

        public FinderResult<Fight> FindFight(Fighter fighterFromFight)
        {
            foreach (Fight fight in exhibition.Fights.ToList())
            {
                if (FighterFinder.FindFighter(fight.GetFighters(), fighterFromFight.FullName).IsFound())
                    return new FinderResult<Fight>(fight);
            }
            return new FinderResult<Fight>(null);
        }
    }
}
