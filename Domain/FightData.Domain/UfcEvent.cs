using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain
{
    public class UfcEvent
    {
        private FightPicksContext context;

        public UfcEvent () { }

        public UfcEvent(string EventName) : this(EventName, new FightPicksContext()) { }

        public UfcEvent(string EventName, FightPicksContext context)
        {
            this.context = context;
            this.EventName = EventName;
            CancelledFighterNames = new List<string>();
            FightersWithMatchingLastNames = new List<Fighter>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public List<Fight> Fights { get; set; }
        public List<Webpage> Webpages { get; set; }
        public List<string> CancelledFighterNames { get; set; }
        public List<Fighter> FightersWithMatchingLastNames { get; set; }

        public void Add()
        {
            context.Events.Add(this);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public List<Fighter> GetAllFighters()
        {
            List<Fighter> winners = new List<Fighter>();
            Fights.ForEach(f => winners.AddRange(new List<Fighter> { f.Winner, f.Loser }));
            return winners;
        }
    }
}
