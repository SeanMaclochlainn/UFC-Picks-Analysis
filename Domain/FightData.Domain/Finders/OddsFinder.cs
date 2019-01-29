using FightData.Domain.Entities;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class OddsFinder : DataFinder
    {
        private FightFinder fightFinder;

        public OddsFinder(FightPicksContext context) : base(context)
        {
            fightFinder = new FightFinder(context);
        }

        public FinderResult<Odd> FindFighterOdd(Fighter fighter, Exhibition exhibition)
        {
            Fight fight = fightFinder.FindFight(fighter, exhibition).Result;
            return new FinderResult<Odd>(fight.Odds.SingleOrDefault(o => o.Fighter == fighter));
        }

        public static FinderResult<Odd> GetWinnerOdds(Fight fight)
        {
            return new FinderResult<Odd>(fight.Odds.SingleOrDefault(o => o.Fighter.Id == fight.Winner.Id));
        }

        public static FinderResult<Odd> GetLoserOdds(Fight fight)
        {
            return new FinderResult<Odd>(fight.Odds.SingleOrDefault(o => o.Fighter.Id == fight.Loser.Id));
        }
    }
}
