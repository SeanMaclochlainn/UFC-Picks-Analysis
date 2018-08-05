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

        public void AddFights(List<RawFightResult> rawFightResults)
        {
            foreach (RawFightResult fightResult in rawFightResults)
                AddFight(fightResult);
        }

        public void AddFight(RawFightResult rawFightResult)
        {
            AddFighter(rawFightResult.Winner);
            AddFighter(rawFightResult.Loser);
            Fighter winner = fighterFinder.FindFighter(rawFightResult.Winner).Result;
            Fighter loser = fighterFinder.FindFighter(rawFightResult.Loser).Result;
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
