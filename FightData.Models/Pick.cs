using System;
using System.Collections.Generic;

namespace FightData.Models
{
    public class Pick
    {
        public int Id { get; set; }
        public Analyst Analyst { get; set; }
        public Fight Fight { get; set; }
        public Fighter FighterPick { get; set; }
    }
}
