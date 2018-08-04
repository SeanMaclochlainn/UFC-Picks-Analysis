using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class FightGenerator
    {
        private FightPicksContext context;
        private UfcEventGenerator ufcEventGenerator;
        private FighterGenerator fighterGenerator;

        public FightGenerator(FightPicksContext context)
        {
            this.context = context;
            ufcEventGenerator = new UfcEventGenerator(context);
            fighterGenerator = new FighterGenerator(context);
        }

        public Fight GetPopulatedFight()
        {
            Fight fight = new Fight(context);
            fight.UfcEvent = ufcEventGenerator.GetEmptyUfcEvent();
            fight.Winner = fighterGenerator.GetWinner();
            fight.Loser = fighterGenerator.GetLoser();
            return fight;
        }

        public Fight GetEmptyFight()
        {
            return new Fight(context);
        }
    }
}
