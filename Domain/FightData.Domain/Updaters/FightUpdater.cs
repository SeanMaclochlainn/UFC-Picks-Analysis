using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain
{
    public class FightUpdater
    {
        private FightPicksContext context;
        private FighterFinder fighterFinder;

        public FightUpdater(FightPicksContext context)
        {
            this.context = context;
            fighterFinder = new FighterFinder(context);
        }

        public void AddFights(List<RawFightResult> rawFightResults, Exhibition exhibition)
        {
            foreach (RawFightResult rawFightResult in rawFightResults)
            {
                EnsureFighterIsAdded(rawFightResult.Winner);
                EnsureFighterIsAdded(rawFightResult.Loser);
                Fighter winner = fighterFinder.FindFighter(rawFightResult.Winner).Result;
                Fighter loser = fighterFinder.FindFighter(rawFightResult.Loser).Result;
                AddFight(winner, loser, exhibition);
            }
        }

        public void DeleteFights(List<Fight> fights)
        {
            context.Fights.RemoveRange(fights);
            context.SaveChanges();
        }

        private void EnsureFighterIsAdded(string name)
        {
            if (!fighterFinder.FindFighter(name).IsFound())
            {
                new FighterUpdater(context).AddFighter(name);
            }
        }

        private void AddFight(Fighter winner, Fighter loser, Exhibition exhibition)
        {
            Fight fight = new Fight(context);
            fight.Winner = winner;
            fight.Loser = loser;
            fight.Exhibition = exhibition;
            fight.Add();
        }
    }
}
