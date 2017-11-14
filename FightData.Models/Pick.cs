using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Models
{
    public class Pick
    {
        public int Id { get; set; }
        public int AnalystId { get; set; }
        public bool Correct { get; set; }
        public int FightId { get; set; }
        public int FighterPickId { get; set; }
    }
}
