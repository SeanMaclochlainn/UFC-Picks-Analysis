using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class EventFinder : DataFinder
    {
        public EventFinder() { }

        public EventFinder(FightPicksContext context) : base(context) { }

        public List<UfcEvent> GetAllEvents()
        {
            return context.UfcEvents.ToList();
        }
    }
}
