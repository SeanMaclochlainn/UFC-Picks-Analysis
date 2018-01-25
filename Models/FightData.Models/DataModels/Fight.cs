using System;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Models.DataModels
{
    public class Fight
    {
        public int Id { get; set; }
        public Fighter Winner { get; set; }
        public Fighter Loser { get; set; }
        public Event Event { get; set; }
        public CardType CardType { get; set; }
        public List<Pick> Picks { get; set; }

        public static bool FightInList(List<Fight> fights, Fight fight)
        {
            List<Fight> matchingFights = fights
                .Where(f =>
                f.Event.Id == fight.Event.Id && 
                f.Winner.Id == fight.Winner.Id && 
                f.Loser.Id == fight.Loser.Id)
                .ToList();

            if (matchingFights.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
