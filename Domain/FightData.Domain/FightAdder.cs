using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain
{
    public class FightAdder
    {
        private FightPicksContext context;
        private UfcEvent ufcEvent;
        private FighterFinder fighterFinder;

        public FightAdder(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
            context = ufcEvent.Context;
            fighterFinder = new FighterFinder(context);
        }

        public void AddFights(List<FightResult> fightResults)
        {
            foreach (FightResult fightResult in fightResults)
                AddFight(fightResult);
        }

        public void AddFight(FightResult fightResult)
        {
            AddFighter(fightResult.Winner);
            AddFighter(fightResult.Loser);
            Fighter winner = fighterFinder.FindFighter(fightResult.Winner).Result;
            Fighter loser = fighterFinder.FindFighter(fightResult.Loser).Result;
            ufcEvent.AddFight(winner, loser);
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
    }
}
