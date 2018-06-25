using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain
{
    public class Fighter
    {
        public Fighter()
        {
            FighterAltNames = new List<FighterAltName>();
            Wins = new List<Fight>();
            Losses = new List<Fight>();
            Picks = new List<Pick>();
        }


        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
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

        }
    }
}
