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

        public PickAdder(UfcEvent ufcEvent)
        {
            context = ufcEvent.Context;
            analystFinder = new AnalystFinder(context);
            fighterFinder = FighterFinder.WithinEvent(ufcEvent, context);
            fightFinder = FightFinder.WithinEvent(ufcEvent, context);
        }

        public void AddPick(Analyst analyst, string pickName)
        {
            Pick pick = new Pick(context);
            pick.Analyst = analyst;
            pick.Fighter = fighterFinder.FindFighter(pickName).Result; 
            pick.Fight = fightFinder.FindFight(pick.Fighter).Result;
            pick.Add();
        }
        
    }
}
