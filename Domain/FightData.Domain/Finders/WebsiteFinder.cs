using FightData.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class WebsiteFinder : DataFinder
    {
        public WebsiteFinder(FightPicksContext context) : base(context) { }

        public List<Website> GetAllWebsites()
        {
            return context.Websites.ToList();
        }

        public Website FindWebsite(WebsiteName websiteName)
        {
            return context.Websites.Single(w => w.WebsiteName == websiteName);
        }
    }
}
