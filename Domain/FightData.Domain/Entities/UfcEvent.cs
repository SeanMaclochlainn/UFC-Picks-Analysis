using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class UfcEvent : Entity
    {
        public UfcEvent(FightPicksContext context) : base(context)
        {
            this.context = context;
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
    }
}
