using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain
{
    public class UfcEventFinder : DataFinder
    {
        public UfcEventFinder() { }

        public UfcEventFinder(FightPicksContext context) : base(context) { }

        public List<UfcEvent> GetAllEvents()
        {
            return context.UfcEvents.ToList();
        }
    }
}
