using FightData.Domain.Finders;
using System.Collections.Generic;
using System.Linq;

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
            fight.Exhibition = this;
            Fights.Add(fight);
        }

        public void Add()
        {
            Context.Exhibitions.Add(this);
            AddWebsitesToContext();
            Context.SaveChanges();
        }

        private void AddWebsitesToContext()
        {
            Context.Websites.AttachRange(Webpages.Select(w => w.Website));
        }

        public void Update()
        {
            Context.SaveChanges();
        }

        public Webpage GetResultsPage()
        {
            return Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }

        public List<Webpage> GetPicksPages()
        {
            return Webpages.Where(w => w.Website.WebsiteType == WebsiteType.Pick).ToList();
        }

        public string GetWebsiteUrl(WebsiteName websiteName)
        {
            return Webpages.Any(w => w.Website.WebsiteName == websiteName) ? Webpages.Single(wp => wp.Website.WebsiteName == websiteName).Url : "";
        }
    }
}
