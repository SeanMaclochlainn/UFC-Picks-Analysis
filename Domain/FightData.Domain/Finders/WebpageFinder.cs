using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class WebpageFinder : DataFinder
    {
        public WebpageFinder(FightPicksContext context) : base(context) { }

        public Webpage GetResultsPage(Exhibition exhibition)
        {
            return exhibition.Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }

        public List<Webpage> GetPicksPages(Exhibition exhibition)
        {
            return exhibition.Webpages.Where(w => w.Website.WebsiteType == WebsiteType.Pick).ToList();
        }

        public Webpage GetPicksPage(Exhibition exhibition, Website website)
        {
            return exhibition.Webpages.Single(w => w.Website == website);
        }

        public Webpage GetOddsPage(Exhibition exhibition)
        {
            return exhibition.Webpages.Single(w => w.Website.WebsiteType == WebsiteType.Odds);
        }

        public Webpage GetWebpage(Exhibition exhibition, Website website)
        {
            return exhibition.Webpages.Single(w => w.Website == website);
        }

    }
}
