using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataProcessor.FightData.Domain
{
    public class FightAdder
    {
        private FightPicksContext context;
        private UfcEvent ufcEvent;

        public FightAdder(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.context = context;
            this.ufcEvent = ufcEvent;
        }

        public void AddFight(string winnerName, string loserName)
        {
            AddFighter(winnerName);
            AddFighter(loserName);
            AddFight(GetFighter(winnerName), GetFighter(loserName));
        }

        private FighterFinder FindFighter(string name)
        {
            FighterFinder fighterFinder = new FighterFinder(context);
            fighterFinder.FindFighter(name);
            return fighterFinder;
        }

        private void AddFighter(string name)
        {
            if (!FindFighter(name).Found)
            {
                Fighter fighter = new Fighter(context);
                fighter.PopulateNames(name);
                fighter.Add();
            }
        }

        private Fighter GetFighter(string name)
        {
            return FindFighter(name).Fighter;
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
