using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class WebpageFinder : DataFinder
    {
        private WebpageFinder(FightPicksContext context) : base(context) { }

        public static WebpageFinder WithCustomContext(FightPicksContext context)
        {
            return new WebpageFinder(context);
        }

        public Webpage GetResultsPage(Exhibition exhibition)
        {
            return exhibition.Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }

        public List<Webpage> GetAllWebpages()
        {
            return context.Webpages.ToList();
        }

        public Webpage GetWebpage(int exhibitionId, int websiteId)
        {
            return context.Webpages.Single(w => w.Exhibition.Id == exhibitionId && w.Website.Id == websiteId);
        }

        public List<Website> GetAllWebsites()
        {
            return context.Websites.ToList();
        }

        public bool WebpageExists(int exhibitionId, int websiteId)
        {
            return context.Webpages.Any(wp => wp.Exhibition.Id == exhibitionId && wp.Website.Id == websiteId);
        }

    }
}
