using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class Analyst : Entity
    {
        public Analyst(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string Name { get; set; }
        public Website Website { get; set; }
        public List<Pick> Picks { get; set; }
        public List<AnalystAltName> AltNames { get; set; }

        public void Add()
        {
            Context.Analysts.Add(this);
            Context.SaveChanges();
        }

    }
}
