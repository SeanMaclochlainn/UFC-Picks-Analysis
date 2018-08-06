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
        public Exhibition Exhibition { get; set; }
        public CardType CardType { get; set; }
        public List<Pick> Picks { get; set; }

        public List<Fighter> GetFighters()
        {
            return new List<Fighter>() { Winner, Loser };
        }

        public void Add()
        {
            Context.Fights.Add(this);
            Context.SaveChanges();
        }
    }
}
