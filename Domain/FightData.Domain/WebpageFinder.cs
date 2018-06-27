using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain
{
    public class WebpageFinder : DataFinder
    {
        public WebpageFinder()
        {

        }

        public WebpageFinder(FightPicksContext context) : base(context)
        {

        }

        public List<Webpage> GetAllWebpages()
        {
            return context.Webpages.ToList();
        }

        public Webpage GetWebpage(int ufcEventId, int websiteId)
        {
            return context.Webpages.Single(w => w.Event.Id == ufcEventId && w.Website.Id == websiteId);
        }

        public List<Website> GetAllWebsites()
        {
            return context.Websites.ToList();
        }

        public bool WebpageExists(int ufcEventId, int websiteId)
        {
            return context.Webpages.Any(wp => wp.Event.Id == ufcEventId && wp.Website.Id == websiteId);
        }


    }
}
