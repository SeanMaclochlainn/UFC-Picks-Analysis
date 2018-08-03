using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class UfcEvent : Entity
    {
        public UfcEvent(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string EventName { get; set; }
        public List<Fight> Fights { get; set; } = new List<Fight>();
        public List<Webpage> Webpages { get; set; }
        public List<string> CancelledFighterNames { get; set; }
        public List<Fighter> FightersWithMatchingLastNames { get; set; }

        public List<Fighter> GetFighters()
        {
            List<Fighter> fighters = new List<Fighter>();
            foreach (Fight fight in Fights)
            {
                fighters.Add(fight.Winner);
                fighters.Add(fight.Loser);
            }
            return fighters;
        }

        public void AddFight(Fighter winner, Fighter loser)
        {
            Fight fight = new Fight(Context);
            fight.Winner = winner;
            fight.Loser = loser;
            fight.UfcEvent = this;
            Fights.Add(fight);
        }

        public void Add()
        {
            Context.UfcEvents.Add(this);
            Context.SaveChanges();
        }

        public void Update()
        {
            Context.SaveChanges();
        }

        public Webpage GetResultsPage()
        {
            return Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }
    }
}
