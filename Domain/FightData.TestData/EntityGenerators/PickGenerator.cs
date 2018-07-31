using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class PickGenerator : EntityGenerator
    {
        private AnalystGenerator analystGenerator;
        private FightGenerator fightGenerator;
        private FighterGenerator fighterGenerator;

        public PickGenerator(FightPicksContext context) : base(context)
        {
            analystGenerator = new AnalystGenerator(context);
            fightGenerator = new FightGenerator(context);
            fighterGenerator = new FighterGenerator(context);
        }

        public Pick GetPick()
        {
            Pick pick = new Pick(context);
            pick.Analyst = analystGenerator.GetAnalyst();
            pick.Fight = fightGenerator.GetFight();
            pick.Fighter = fighterGenerator.GetFighter();
            return pick;
        }
    }
}
