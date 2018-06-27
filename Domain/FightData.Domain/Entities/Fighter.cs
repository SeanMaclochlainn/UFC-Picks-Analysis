using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class Fighter : Entity
    {
        public Fighter()
        {
        }

        public Fighter(string fullName) : this(fullName, new FightPicksContext()) { }

        public Fighter(string fullName, FightPicksContext context) : base(context)
        {
            FullName = fullName;
            PopulateNames();
        }

        public int Id { get; set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public List<FighterAltName> FighterAltNames { get; set; }
        public List<Fight> Wins { get; set; }
        public List<Fight> Losses { get; set; }
        public List<Pick> Picks { get; set; }

        public static bool IsFighterInList(List<Fighter> fighters, Fighter fighter)
        {
            int count = fighters.Count(f => f.FullName == fighter.FullName);
            if (count > 0)
                return true;
            else
                return false;
        }

        public void Add()
        {
            context.Fighters.Add(this);
            context.SaveChanges();
        }

        private void PopulateNames()
        {
            NameParser nameParser = new NameParser(FullName);
            FirstName = nameParser.FirstName;
            LastName = nameParser.LastName;
            MiddleName = nameParser.MiddleNames;
        }


    }
}
