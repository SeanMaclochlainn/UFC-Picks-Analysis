using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain
{
    public class FightUpdater
    {
        private FightPicksContext context;
        private Exhibition exhibition;
        private FighterFinder fighterFinder;
        private Fighter winner;
        private Fighter loser;

        public FightUpdater(Exhibition exhibition)
        {
            this.exhibition = exhibition;
            context = exhibition.Context;
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
                new FighterUpdater(context).AddFighter(name);
            }
        }

        private void AddFight()
        {
            Fight fight = new Fight(context);
            fight.Winner = winner;
            fight.Loser = loser;
            fight.Exhibition = exhibition;
            fight.Add();
        }
    }
}
