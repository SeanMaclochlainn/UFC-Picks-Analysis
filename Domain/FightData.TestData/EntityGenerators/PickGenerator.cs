using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class PickGenerator
    {
        private FightPicksContext context;

        public PickGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Pick GetPopulatedPick()
        {
            Pick pick = new Pick(context);
            pick.Analyst = new AnalystGenerator(context).GetPopulatedAnalyst();
            pick.Fight = new FightGenerator(context).GetPopulatedFight();
            pick.Fighter = new FighterGenerator(context).GetPopulatedFighter();
            return pick;
        }
    }
}
