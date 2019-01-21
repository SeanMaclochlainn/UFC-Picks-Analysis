using FightData.Domain.Entities;

namespace FightData.Domain.Updaters
{
    public class WebsiteUpdater
    {
        private FightPicksContext context;

        public WebsiteUpdater(FightPicksContext context)
        {
            this.context = context;
        }

        public void Add(Website website)
        {
            context.Websites.Add(website);
            context.SaveChanges();
        }
    }
}
