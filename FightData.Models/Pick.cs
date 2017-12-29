using System;
using System.Collections.Generic;

namespace FightData.Models
{
    public class Pick
    {
        public int Id { get; set; }
        public Analyst Analyst { get; set; }
        public string Pick1 { get; set; }
        public bool? Correct { get; set; }
        public Fight Fight { get; set; }
        public int FighterPickId { get; set; }
    }
}
