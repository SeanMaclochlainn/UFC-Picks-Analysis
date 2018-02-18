using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class Event
    {
        public Event()
        {
            CancelledFighterNames = new List<string>();
            FightersWithMatchingLastNames = new List<Fighter>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public List<Fight> Fights { get; set; }
        public List<Webpage> Webpages { get; set; }
        
        public List<Fighter> GetAllFighters()
        {
            List<Fighter> winners = new List<Fighter>();
            Fights.ForEach(f => winners.AddRange(new List<Fighter> { f.Winner, f.Loser }));
            return winners;
        }

        public List<string> CancelledFighterNames { get; set; }
        public List<Fighter> FightersWithMatchingLastNames { get; set; }


    }
}
