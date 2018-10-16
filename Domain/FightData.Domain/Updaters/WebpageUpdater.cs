using FightData.Domain.Entities;

namespace FightData.Domain.Updaters
{
    public class WebpageUpdater
    {
        private Webpage webpage;
        private FightPicksContext context;

        public WebpageUpdater(Webpage webpage)
        {
            this.webpage = webpage;
            context = webpage.Context;
        }

        public void MarkAsParsed()
        {
            webpage.Parsed = true;
            context.SaveChanges();
        }

        public void MarkAsUnparsed()
        {
            webpage.Parsed = false;
            context.SaveChanges();
        }
    }
}
