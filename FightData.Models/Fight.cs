using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Models
{
    public class Fight
    {
        public int Id { get; set; }
        public Fighter FighterA { get; set; }
        public Fighter FighterB { get; set; }
        public Fighter Winner { get; set; }
        public Event Event { get; set; }
        public int CardTypeId { get; set; }
    }
}
