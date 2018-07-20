using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class UfcEvent : Entity
    {
        public UfcEvent () { }

        public UfcEvent(string EventName) : this(EventName, new FightPicksContext()) { }

        public UfcEvent(string EventName, FightPicksContext context) : base(context)
        {
            this.context = context;
            this.EventName = EventName;
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public List<Fight> Fights { get; set; }
        public List<Webpage> Webpages { get; set; }
        public List<string> CancelledFighterNames { get; set; }
        public List<Fighter> FightersWithMatchingLastNames { get; set; }

        public void Add()
        {
            context.UfcEvents.Add(this);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public Webpage GetResultsPage()
        {
            return Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }

        public List<Fighter> GetAllFighters()
        {
            List<Fighter> winners = new List<Fighter>();
            Fights.ForEach(f => winners.AddRange(new List<Fighter> { f.Winner, f.Loser }));
            return winners;
        }
    }
}
