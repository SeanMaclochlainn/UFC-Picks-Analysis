using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FighterFinder : DataFinder
    {
        private Exhibition exhibition;
        private bool searchWithinExhibition;

        public FighterFinder(FightPicksContext context) : base(context) { }

        private FighterFinder(Exhibition exhibition, FightPicksContext context) : this(context)
        {
            this.exhibition = exhibition;
            searchWithinExhibition = true;
        }

        public static FighterFinder WithinExhibition(Exhibition exhibition, FightPicksContext context)
        {
            return new FighterFinder(exhibition, context);
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
            if (searchWithinExhibition)
                return exhibition.GetFighters();
            else
                return context.Fighters.ToList();
        }


    }
}
