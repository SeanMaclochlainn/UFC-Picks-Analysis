using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain
{
    public class PickAdder
    {
        private AnalystFinder analystFinder;
        private FightPicksContext context;
        private FighterFinder fighterFinder;
        private FightFinder fightFinder;

        public PickAdder(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.context = context;
            analystFinder = new AnalystFinder(context);
            fighterFinder = FighterFinder.WithinEvent(ufcEvent, context);
            fightFinder = FightFinder.WithinEvent(ufcEvent, context);
        }

        public void AddPick(string analystName, string pickName)
        {
            Pick pick = new Pick(context);
            pick.Analyst = analystFinder.FindAnalyst(analystName).Result;
            pick.Fighter = fighterFinder.FindFighter(pickName).Result; 
            pick.Fight = fightFinder.FindFight(pick.Fighter).Result;
            pick.Add();
        }
        
    }
}
