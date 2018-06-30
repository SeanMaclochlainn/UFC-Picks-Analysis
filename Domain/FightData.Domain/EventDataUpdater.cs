using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain
{
    public class EventDataUpdater
    {
        private UfcEvent ufcEvent;
        private FightPicksContext context;

        public EventDataUpdater(UfcEvent ufcEvent) : this(ufcEvent, new FightPicksContext()) { }

        public EventDataUpdater(UfcEvent ufcEvent, FightPicksContext context)
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
            FighterFinder fighterFinder = new FighterFinder(name, context);
            if(!fighterFinder.FighterExists)
                AddFighter(name);
        }

        private void AddFighter(string name)
        {
            Fighter fighter = new Fighter(name, context);
            fighter.Add();
        }
    }
}
