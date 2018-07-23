﻿using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class Fighter : Entity
    {
        public Fighter(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public List<FighterAltName> FighterAltNames { get; set; }
        public List<Fight> Wins { get; set; }
        public List<Fight> Losses { get; set; }
        public List<Pick> Picks { get; set; }

        public void Add()
        {
            context.Fighters.Add(this);
            context.SaveChanges();
        }

        public void PopulateNames(string fullName)
        {
            FullName = fullName;
            NameParser nameParser = new NameParser(FullName);
            FirstName = nameParser.FirstName;
            LastName = nameParser.LastName;
            MiddleName = nameParser.MiddleNames;
        }


    }
}