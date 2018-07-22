using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.FightData.Domain
{
    public class FightAdder
    {
        private FightPicksContext context;
        private UfcEvent ufcEvent;
        private FighterFinder fighterFinder;

        public FightAdder(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.context = context;
            this.ufcEvent = ufcEvent;
            fighterFinder = new FighterFinder(context);
        }

        public void AddFight(string winnerName, string loserName)
        {
            AddFighter(winnerName);
            AddFighter(loserName);
            AddFight(fighterFinder.FindFighter(winnerName).Result, fighterFinder.FindFighter(loserName).Result);
        }

        private void AddFighter(string name)
        {
            if (!fighterFinder.FindFighter(name).IsFound())
            {
                Fighter fighter = new Fighter(context);
                fighter.PopulateNames(name);
                fighter.Add();
            }
        }

        private void AddFight(Fighter winner, Fighter loser)
        {
            Fight fight = new Fight(context);
            fight.Winner = winner;
            fight.Loser = loser;
            fight.UfcEvent = ufcEvent;
            fight.Add();
        }
    }
}
