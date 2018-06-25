using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain
{
    public class DataFinder
    {
        protected FightPicksContext context;

        public DataFinder()
        {
            context = new FightPicksContext();
        }

        public DataFinder(FightPicksContext context)
        {
            this.context = context;
        }
    }
}
