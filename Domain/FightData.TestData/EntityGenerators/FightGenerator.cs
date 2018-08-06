using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class FightGenerator
    {
        private FightPicksContext context;

        public FightGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Fight GetPopulatedFight()
        {
            Fight fight = new Fight(context);
            fight.Exhibition = new ExhibitionGenerator(context).GetEmptyExhibition();
            fight.Winner = new FighterGenerator(context).GetWinner();
            fight.Loser = new FighterGenerator(context).GetLoser();
            return fight;
        }

        public Fight GetEmptyFight()
        {
            return new Fight(context);
        }
    }
}
