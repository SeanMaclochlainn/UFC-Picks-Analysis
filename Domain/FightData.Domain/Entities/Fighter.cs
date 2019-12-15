using FightData.Domain.Builders;
using System.Collections.Generic;

namespace FightData.Domain.Entities
{
    public class Fighter : Entity
    {
        public Fighter(FightPicksContext context) : base(context) { }

        public Fighter(FighterBuilder fighterBuilder) : this(fighterBuilder.Context)
        {
            FullName = fighterBuilder.FullName;
            FirstName = fighterBuilder.FirstName;
            LastName = fighterBuilder.LastName;
            MiddleName = fighterBuilder.MiddleName;
        }

        public int Id { get; set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public List<FighterAltName> FighterAltNames { get; set; }
        public List<Fight> Wins { get; set; }
        public List<Fight> Losses { get; set; }
        public List<Pick> Picks { get; set; }
        public List<Odd> Odds { get; set; }
    }
}
