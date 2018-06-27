using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Entities
{
    public class AnalystAltName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Analyst Analyst { get; set; }
    }
}
