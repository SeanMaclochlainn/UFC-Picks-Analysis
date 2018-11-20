using System;
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
            name = new NameParser(name).GetFullName();
            FinderResult<Fighter> result = FindFighter(f => f.FullName == name, specifiedFighters);
            if (!result.IsFound())
                result = FindFighter(f => f.LastName == name, specifiedFighters);
            return result;
        }

        private static FinderResult<Fighter> FindFighter(Func<Fighter, bool> fighterNameFunc, List<Fighter> fighters)
        {
            if (fighters.Count(fighterNameFunc) > 1)
                return new FinderResult<Fighter>(null);
            else
                return new FinderResult<Fighter>(fighters.SingleOrDefault(fighterNameFunc));
        }

        public FinderResult<Fighter> FindFighter(string name)
        {
            return FindFighter(GetAllFighters(), name);
        }

        public FinderResult<Fighter> FindFighter(int id)
        {
            return new FinderResult<Fighter>(context.Fighters.Find(id));
        }

        public FinderResult<Fighter> FindFighter(string name, Exhibition exhibition)
        {
            return FindFighter(GetFighters(exhibition), name);
        }

        public List<Fighter> GetAllFighters()
        {
            return context.Fighters.ToList();
        }

        public static List<Fighter> GetFighters(Exhibition exhibition)
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
