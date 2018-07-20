using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain
{
    public class EventUpdater
    {
        private UfcEvent ufcEvent;
        private FightPicksContext context;

        public EventUpdater(UfcEvent ufcEvent) : this(ufcEvent, new FightPicksContext()) { }

        public EventUpdater(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.ufcEvent = ufcEvent;
            this.context = context;
        }

        public void AddFightData(string winner, string loser)
        {
            ProcessFighterDetails(winner);
            ProcessFighterDetails(loser);
        }

        private void ProcessFighterDetails(string name)
        {
            FighterFinder fighterFinder = FighterFinder.WithCustomContext(context);
            fighterFinder.FindFighter(name);
            if(!fighterFinder.Found)
                AddFighter(name);
        }

        private void AddFighter(string name)
        {
            Fighter fighter = new Fighter(name, context);
            fighter.Add();
        }
    }
}
