using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class Fight : Entity
    {
        public Fight(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public Fighter Winner { get; set; }
        public Fighter Loser { get; set; }
        public UfcEvent UfcEvent { get; set; }
        public CardType CardType { get; set; }
        public List<Pick> Picks { get; set; }

        public List<Fighter> GetFighters()
        {
            return new List<Fighter>() { Winner, Loser };
        }

        public void Add()
        {
            context.Fights.Add(this);
            context.SaveChanges();
        }
    }
}
