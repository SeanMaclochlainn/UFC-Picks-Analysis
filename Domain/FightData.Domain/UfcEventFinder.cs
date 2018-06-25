using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain
{
    public class UfcEventFinder : DataFinder
    {
        public UfcEventFinder() { }

        public UfcEventFinder(FightPicksContext context) : base(context) { }

        public List<UfcEvent> GetAllEvents()
        {
            return context.Events.ToList();
        }
    }
}
