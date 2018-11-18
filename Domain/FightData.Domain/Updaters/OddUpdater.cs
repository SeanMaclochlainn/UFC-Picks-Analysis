using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Domain.Updaters
{
    public class OddUpdater
    {
        private FightPicksContext context;

        public OddUpdater(FightPicksContext context)
        {
            this.context = context;
        }

        public void AddOdds(List<Odd> odds)
        {
            foreach (Odd odd in odds)
                context.Odds.Add(odd);
            context.SaveChanges();
        }
    }
}
