using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class FighterGenerator : EntityGenerator
    {
        public FighterGenerator(FightPicksContext context) : base(context) { }

        public Fighter GetPopulatedFighter()
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames("testfname testlname");
            return fighter;
        }
    }
}
