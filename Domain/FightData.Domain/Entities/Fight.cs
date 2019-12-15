using System.Collections.Generic;
using FightData.Domain.Builders;

namespace FightData.Domain.Entities
{
    public class Fight : Entity
    {
        public Fight(FightPicksContext context) : base(context) { }

        public Fight(FightBuilder fightBuilder) : this(fightBuilder.Context)
        {
            Winner = fightBuilder.Winner;
            Loser = fightBuilder.Loser;
            Exhibition = fightBuilder.Exhibition;
        }

        public int Id { get; set; }
        public Fighter Winner { get; set; }
        public Fighter Loser { get; set; }
        public Exhibition Exhibition { get; set; }
        public CardType CardType { get; set; }
        public List<Pick> Picks { get; set; } = new List<Pick>();
        public List<Odd> Odds { get; set; } = new List<Odd>();
    }
}
