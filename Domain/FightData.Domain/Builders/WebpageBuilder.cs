using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain.Builders
{
    public class WebpageBuilder
    {
        private EntityFinder entityFinder;

        public WebpageBuilder(FightPicksContext context)
        {
            Context = context;
            entityFinder = new EntityFinder(context);
        }

        public FightPicksContext Context { get; private set; }
        public string Url { get; private set; }
        public Website Website { get; private set; }
        public Exhibition Exhibition { get; private set; }
        public string Data { get; private set; }
        public bool Parsed { get; private set; }

        public WebpageBuilder GenerateSampleParsedResultsWebpage(Exhibition exhibition, string data)
        {
            Exhibition = exhibition;
            Data = data;
            Parsed = true;
            Website = entityFinder.WebsiteFinder.FindWebsite(WebsiteName.Wikipedia);
            Url = "www.resultspage.com";
            return this;
        }

        public WebpageBuilder GenerateSampleUnparsedResultsWebpage(Exhibition exhibition, string data)
        {
            PopulateBasicEntities(exhibition, data);
            Parsed = false;
            PopulateSampleResultPageEntities();
            return this;
        }

        public WebpageBuilder GenerateSampleParsedPicksWebpage(Exhibition exhibition, string data, WebsiteName websiteName)
        {
            PopulateBasicEntities(exhibition, data);
            Parsed = true;
            PopulateSamplePicksPageEntities(websiteName);
            return this;
        }

        public WebpageBuilder GenerateSampleUnparsedPicksWebpage(Exhibition exhibition, string data, WebsiteName websiteName)
        {
            PopulateBasicEntities(exhibition, data);
            Parsed = false;
            PopulateSamplePicksPageEntities(websiteName);
            return this;
        }

        public WebpageBuilder GenerateSampleParsedOddsWebpage(Exhibition exhibition, string data)
        {
            PopulateBasicEntities(exhibition, data);
            Parsed = true;
            PopluateSampleOddsPageEntities();
            return this;
        }

        public WebpageBuilder GenerateSampleUnparsedOddsWebpage(Exhibition exhibition, string data)
        {
            PopulateBasicEntities(exhibition, data);
            Parsed = false;
            PopluateSampleOddsPageEntities();
            return this;
        }

        public WebpageBuilder GenerateEmptyWebpage(string url, Website website)
        {
            Url = url;
            Website = website;
            Parsed = false;
            return this;
        }

        public Webpage Build()
        {
            return new Webpage(this);
        }

        private void PopulateBasicEntities(Exhibition exhibition, string data)
        {
            Exhibition = exhibition;
            Data = data;
        }

        private void PopulateSampleResultPageEntities()
        {
            Website = entityFinder.WebsiteFinder.FindWebsite(WebsiteName.Wikipedia);
            Url = "www.resultspage.com";
        }

        private void PopulateSamplePicksPageEntities(WebsiteName websiteName)
        {
            Website = entityFinder.WebsiteFinder.FindWebsite(websiteName);
            Url = "www.pickspage.com";
        }

        private void PopluateSampleOddsPageEntities()
        {
            Website = entityFinder.WebsiteFinder.FindWebsite(WebsiteName.BestFightOdds);
            Url = "www.oddspage.com";
        }


    }
}
