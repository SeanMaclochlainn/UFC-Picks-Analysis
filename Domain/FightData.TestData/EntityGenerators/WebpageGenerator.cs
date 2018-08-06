using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData.EntityGenerators
{
    public class WebpageGenerator
    {
        private FightPicksContext context;

        public WebpageGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Webpage GetPopulatedResultsPage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = new WebsiteGenerator(context).GetResultsPageWebsite(),
                Data = HtmlPageGenerator.GetWikipediaPage(),
                Exhibition = new ExhibitionGenerator(context).GetEmptyExhibition()
            };
            return webpage;
        }

        public Webpage GetPopulatedPicksPage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "https://mmajunkie.com/2014/11/ufc-fight-night-55-staff-picks-rockhold-a-unanimous-nod-over-bisping",
                Website = new WebsiteGenerator(context).GetPicksPageWebsite(),
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
                Website = new WebsiteGenerator(context).GetResultsPageWebsite(),
                Data = "test data"
            };
            return webpage;
        }
    }
}
