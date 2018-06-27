using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightData.Domain.Entities
{
    public class Fight : Entity
    {
        public Fight() { }

        public Fight(FightPicksContext context) : base(context) { }

        public Fight(Fighter winner, Fighter loser, UfcEvent ufcEvent) : this(new FightPicksContext(), winner, loser, ufcEvent) { }

        public Fight(FightPicksContext context, Fighter winner, Fighter loser, UfcEvent ufcEvent) : base(context)
        {
            Winner = winner;
            Loser = loser;
            UfcEvent = ufcEvent;
        }

        public int Id { get; set; }
        public Fighter Winner { get; set; }
        public Fighter Loser { get; set; }
        public UfcEvent UfcEvent { get; set; }
        public CardType CardType { get; set; }
        public List<Pick> Picks { get; set; }

        public List<Fighter> GetAllFighters()
        {
            return new List<Fighter>() { Winner, Loser };
        }

        public static bool FightInList(List<Fight> fights, Fight fight)
        {
            List<Fight> matchingFights = fights
                .Where(f =>
                f.UfcEvent.Id == fight.UfcEvent.Id &&
                f.Winner.Id == fight.Winner.Id &&
                f.Loser.Id == fight.Loser.Id)
                .ToList();

            if (matchingFights.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void Add()
        {
            context.Fights.Add(this);
            context.SaveChanges();
        }
    }
}
