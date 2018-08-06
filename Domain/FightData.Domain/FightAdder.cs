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
        private Fighter winner;
        private Fighter loser;

        public FightAdder(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
            context = ufcEvent.Context;
            fighterFinder = new FighterFinder(context);
        }

        public void AddFights(List<RawFightResult> rawFightResults)
        {
            foreach (RawFightResult rawFightResult in rawFightResults)
                AddFight(rawFightResult);
        }

        public void AddFight(RawFightResult rawFightResult)
        {
            EnsureFighterIsAdded(rawFightResult.Winner);
            EnsureFighterIsAdded(rawFightResult.Loser);
            winner = fighterFinder.FindFighter(rawFightResult.Winner).Result;
            loser = fighterFinder.FindFighter(rawFightResult.Loser).Result;
            AddFight();
        }

        private void EnsureFighterIsAdded(string name)
        {
            if (!fighterFinder.FindFighter(name).IsFound())
            {
                new FighterAdder(context).AddFighter(name);
            }
        }

        private void AddFight()
        {
            Fight fight = new Fight(context);
            fight.Winner = winner;
            fight.Loser = loser;
            fight.UfcEvent = ufcEvent;
            fight.Add();
        }
    }
}
