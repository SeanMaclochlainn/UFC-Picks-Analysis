using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain
{
    public class DataRemover
    {
        private FightPicksContext context;

        public DataRemover()
        {
            context = new FightPicksContext();
        }

        public DataRemover(FightPicksContext context)
        {
            this.context = context;
        }

        public void RemoveAllPicks()
        {
            context.Picks.RemoveRange(context.Picks.ToList());
            context.SaveChanges();
        }
    }
}
