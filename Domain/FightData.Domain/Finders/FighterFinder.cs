using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FighterFinder : DataFinder
    {
        private UfcEvent ufcEvent;
        private bool searchWithinEvent;

        public FighterFinder(FightPicksContext context) : base(context) { }

        private FighterFinder(UfcEvent ufcEvent, FightPicksContext context) : this(context)
        {
            this.ufcEvent = ufcEvent;
            searchWithinEvent = true;
        }

        public static FighterFinder WithinEvent(UfcEvent ufcEvent, FightPicksContext context)
        {
            return new FighterFinder(ufcEvent, context);
        }

        public static FinderResult<Fighter> FindFighter(List<Fighter> specifiedFighters, string name)
        {
            Fighter fighter = specifiedFighters.SingleOrDefault(f => f.FullName == name);
            if (fighter == null)
                fighter = specifiedFighters.SingleOrDefault(f => f.LastName == name);
            return new FinderResult<Fighter>(fighter);
        }

        public FinderResult<Fighter> FindFighter(string name)
        {
            return FindFighter(GetFightersToSearch(), name);
        }

        private List<Fighter> GetFightersToSearch()
        {
            if (searchWithinEvent)
                return ufcEvent.GetFighters();
            else
                return context.Fighters.ToList();
        }


    }
}
