using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FightFinder : DataFinder
    {
        private UfcEvent ufcEvent;
        private FighterFinder fighterFinder;

        private FightFinder(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.ufcEvent = ufcEvent;
            fighterFinder = new FighterFinder(context);
        }

        public static FightFinder WithinEvent(UfcEvent ufcEvent, FightPicksContext context)
        {
            return new FightFinder(ufcEvent, context);
        }

        public FinderResult<Fight> FindFight(Fighter fighterFromFight)
        {
            foreach(Fight fight in ufcEvent.Fights.ToList())
            {
                if (FighterFinder.FindFighter(fight.GetFighters(), fighterFromFight.FullName).IsFound())
                    return new FinderResult<Fight>(fight);
            }
            return new FinderResult<Fight>(null);
        }
    }
}
