using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class FightGenerator : EntityGenerator
    {
        private UfcEventGenerator ufcEventGenerator;

        public FightGenerator(FightPicksContext context) : base(context)
        {
            ufcEventGenerator = new UfcEventGenerator(context);
        }

        public Fight GetPopulatedFight()
        {
            Fight fight = new Fight(context);
            fight.UfcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            fight.Winner = Fighter.GenerateFighter("Luke Rockhold", context);
            fight.Loser = Fighter.GenerateFighter("Michael Bisping", context);
            return fight;
        }

        public Fight GetEmptyFight()
        {
            return new Fight(context);
        }
    }
}
