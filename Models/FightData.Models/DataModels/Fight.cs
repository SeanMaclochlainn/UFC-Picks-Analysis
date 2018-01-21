using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Fight fight = (Fight)obj;
            if (fight.Winner == Winner && fight.Loser == Loser && fight.Event == Event)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Winner.GetHashCode() ^ Loser.GetHashCode() ^ Event.GetHashCode();
        }
    }
}
