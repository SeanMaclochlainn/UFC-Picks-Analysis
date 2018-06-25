using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain
{
    public class FighterAltName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Fighter Fighter { get; set; }
    }
}
