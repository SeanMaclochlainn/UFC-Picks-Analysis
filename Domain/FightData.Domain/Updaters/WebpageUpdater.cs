using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Domain.Updaters
{
    public class WebpageUpdater
    {
        private FightPicksContext context;

        public WebpageUpdater(FightPicksContext context)
        {
            this.context = context;
        }

        public void MarkAsParsed(Webpage webpage)
        {
            webpage.Parsed = true;
            context.SaveChanges();
        }

        public void MarkAsUnparsed(Webpage webpage)
        {
            webpage.Parsed = false;
            context.SaveChanges();
        }

        public void MarkAsUnparsed(List<Webpage> webpages)
        {
            foreach (Webpage webpage in webpages)
                webpage.Parsed = false;
            context.SaveChanges();
        }

        public void DeleteDownloadedData(Exhibition exhibition)
        {
            foreach(Webpage webpage in exhibition.Webpages)
            {
                webpage.Data = "";
            }
            context.SaveChanges();
        }

        public void AddWebpages(List<Webpage> webpages)
        {
            foreach (Webpage webpage in webpages)
                context.Webpages.Add(webpage);
            context.SaveChanges();
        }
    }
}
