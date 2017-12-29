using System;
using System.Collections.Generic;

namespace FightData.Models
{
    public class Fight
    {
        public int Id { get; set; }
        public int FighterAid { get; set; }
        public int FighterBid { get; set; }
        public int WinnerId { get; set; }
        public Event Event { get; set; }
        public CardType CardType { get; set; }
        public List<Pick> Picks { get; set; }
    }
}
