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
                Data = MockWikipediaPageGenerator.GetHtml()
            };
            return webpage;
        }

        public Webpage GetWebpage()
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
