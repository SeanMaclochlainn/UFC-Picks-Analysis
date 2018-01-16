using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Models
{
    public class FighterAltName
    {
        public int FighterId { get; set; }
        public Fighter Fighter { get; set; }
        public int AltNameId { get; set; }
        public AltName AltName { get; set; }
    }
}
