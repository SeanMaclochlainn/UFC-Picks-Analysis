﻿using System;
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
            NameParser parsedName = new NameParser(name);
            FinderResult<Fighter> result = FindFighter(f => f.FullName == parsedName.GetFullName(), specifiedFighters);
            if (!result.IsFound())
                result = FindFighter(f => $"{f.MiddleName} {f.LastName}" == parsedName.GetFullName(), specifiedFighters);
            if (!result.IsFound())
                result = FindFighter(f => f.LastName == parsedName.GetLastName(), specifiedFighters);
            return result;
        }

        public static FinderResult<Fighter> FindFighterByFullName(List<Fighter> specifiedFighters, string fullName)
        {
            NameParser parsedName = new NameParser(fullName);
            FinderResult<Fighter> result = FindFighter(f => f.FullName == parsedName.GetFullName(), specifiedFighters);
            return result;
        }

        public static FinderResult<Fighter> FindFighter(List<Fighter> specifiedFighters, int fighterId)
        {
            return FindFighter(f => f.Id == fighterId, specifiedFighters);
        }

        private static FinderResult<Fighter> FindFighter(Func<Fighter, bool> fighterFindingMethod, List<Fighter> fighters)
        {
            if (fighters.Count(fighterFindingMethod) > 1)
                return new FinderResult<Fighter>(null);
            else
                return new FinderResult<Fighter>(fighters.SingleOrDefault(fighterFindingMethod));
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

        public FinderResult<Fighter> FindFighter(string fullName)
        {
            return FindFighterByFullName(GetAllFighters(), fullName);
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

        public List<Fighter> GetFighters(Fight fight)
        {
            return new List<Fighter>() { fight.Winner, fight.Loser };
        }

    }
}
