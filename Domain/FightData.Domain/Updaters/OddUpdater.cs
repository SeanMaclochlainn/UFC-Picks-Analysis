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

        public void AddOdd(Odd odd)
        {
            context.Odds.Add(odd);
            context.SaveChanges();
        }

        public void AddOdds(List<Odd> odds)
        {
            context.Odds.AddRange(odds);
            context.SaveChanges();
        }
    }
}
