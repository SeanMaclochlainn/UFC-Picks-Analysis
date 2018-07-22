using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class Analyst
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Website Website { get; set; }
        public List<Pick> Picks { get; set; }
        public List<AnalystAltName> AltNames { get; set; }
    }
}
