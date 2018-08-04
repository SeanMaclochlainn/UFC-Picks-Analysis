using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class UfcEventFinder : DataFinder
    {
        public UfcEventFinder(FightPicksContext context) : base(context) { }

        public List<UfcEvent> FindAllEvents()
        {
            return context.UfcEvents.ToList();
        }
    }
}
