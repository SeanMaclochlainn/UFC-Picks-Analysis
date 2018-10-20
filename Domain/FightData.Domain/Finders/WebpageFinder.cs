using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class WebpageFinder : DataFinder
    {
        public WebpageFinder(FightPicksContext context) : base(context) { }

        public List<Webpage> GetAllWebpages()
        {
            return context.Webpages.ToList();
        }

        public Webpage GetWebpage(int exhibitionId, int websiteId)
        {
            return context.Webpages.Single(w => w.Exhibition.Id == exhibitionId && w.Website.Id == websiteId);
        }

        public Webpage GetResultsPage(Exhibition exhibition)
        {
            return exhibition.Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }

        public List<Webpage> GetPicksPages(Exhibition exhibition)
        {
            return exhibition.Webpages.Where(w => w.Website.WebsiteType == WebsiteType.Pick).ToList();
        }

        public Webpage GetWebpage(Exhibition exhibition, Website website)
        {
            return exhibition.Webpages.Single(w => w.Website == website);
        }

    }
}
