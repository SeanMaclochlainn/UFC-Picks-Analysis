using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FighterFinder : DataFinder
    {
        public FighterFinder(FightPicksContext context) : base(context) { }

        public static FinderResult<Fighter> FindFighter(List<Fighter> specifiedFighters, string name)
        {
            Fighter fighter = specifiedFighters.SingleOrDefault(f => f.FullName == name);
            if (fighter == null)
                fighter = specifiedFighters.SingleOrDefault(f => f.LastName == name);
            return new FinderResult<Fighter>(fighter);
        }

        public FinderResult<Fighter> FindFighter(string name)
        {
            return FindFighter(GetAllFighters(), name);
        }

        public FinderResult<Fighter> FindFighter(string name, Exhibition exhibition)
        {
            return FindFighter(GetFighters(exhibition), name);
        }

        public List<Fighter> GetAllFighters()
        {
            return context.Fighters.ToList();
        }

        public List<Fighter> GetFighters(Exhibition exhibition)
        {
            List<Fighter> fighters = new List<Fighter>();
            foreach (Fight fight in exhibition.Fights)
            {
                fighters.Add(fight.Winner);
                fighters.Add(fight.Loser);
            }
            return fighters;
        }

        public List<Fighter> GetFighters(Fight fight)
        {
            return new List<Fighter>() { fight.Winner, fight.Loser };
        }

    }
}
