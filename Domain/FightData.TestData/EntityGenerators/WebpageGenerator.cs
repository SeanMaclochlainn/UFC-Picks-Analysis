using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class WebpageGenerator : EntityGenerator
    {
        private WebsiteGenerator websiteGenerator;

        public WebpageGenerator(FightPicksContext context) : base(context)
        {
            websiteGenerator = new WebsiteGenerator(context);
        }

        public Webpage GetPopulatedResultsPage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = websiteGenerator.GetResultsPageWebsite(),
                Data = HtmlPageGenerator.GetWikipediaPage()
            };
            return webpage;
        }

        public Webpage GetPopulatedPicksPage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "https://mmajunkie.com/2014/11/ufc-fight-night-55-staff-picks-rockhold-a-unanimous-nod-over-bisping",
                Website = websiteGenerator.GetPicksPageWebsite(),
                Data = HtmlPageGenerator.GetPicksPage(),
                WebpageType = WebpageType.PicksPage
            };
            return webpage;
        }

        public Webpage GetEmptyWebpage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "url",
                Website = websiteGenerator.GetResultsPageWebsite(),
                Data = "test data"
            };
            return webpage;
        }
    }
}
