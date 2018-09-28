using FightData.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class WebsiteFinder : DataFinder
    {
        public WebsiteFinder() { }

        public WebsiteFinder(FightPicksContext context) : base(context) { }

        public List<Website> FindAllWebsites()
        {
            return context.Websites.ToList();
        }
    }
}
