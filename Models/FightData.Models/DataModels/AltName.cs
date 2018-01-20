using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class AltName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FighterAltName> Fighters { get; set; }
    }
}
