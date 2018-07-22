using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class EventFinder : DataFinder
    {
        public EventFinder(FightPicksContext context) : base(context) { }

        public List<UfcEvent> FindAllEvents()
        {
            return context.UfcEvents.ToList();
        }
    }
}
