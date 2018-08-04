using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class FighterGenerator
    {
        private FightPicksContext context;

        public FighterGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Fighter GetPopulatedFighter()
        {
            return Fighter.GenerateFighter("testfname testlname", context);
        }

        public Fighter GetWinner()
        {
            return Fighter.GenerateFighter("Luke Rockhold", context);
        }

        public Fighter GetLoser()
        {
            return Fighter.GenerateFighter("Michael Bisping", context);
        }
    }
}
