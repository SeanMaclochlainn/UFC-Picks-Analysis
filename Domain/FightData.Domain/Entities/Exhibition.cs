using System.Collections.Generic;

namespace FightData.Domain.Entities
{
    public class Exhibition : Entity
    {
        public Exhibition() { }

        public Exhibition(FightPicksContext context, string name, List<Webpage> webpages) : this(context)
        {
            Webpages = webpages;
            Name = name;
        }

        public Exhibition(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fight> Fights { get; set; } = new List<Fight>();
        public List<Webpage> Webpages { get; set; } = new List<Webpage>();
        public List<string> CancelledFighterNames { get; set; }
        public List<Fighter> FightersWithMatchingLastNames { get; set; }
    }
}
