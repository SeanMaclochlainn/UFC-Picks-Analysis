using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class FighterGenerator : EntityGenerator
    {
        public FighterGenerator(FightPicksContext context) : base(context) { }

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
