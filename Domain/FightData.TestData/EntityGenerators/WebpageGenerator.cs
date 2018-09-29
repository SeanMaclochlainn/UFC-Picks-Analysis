﻿using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Linq;

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
                Website = new WebsiteFinder(context).FindAllWebsites().First(w => w.WebsiteType == WebsiteType.Result),
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
                Website = new WebsiteFinder(context).FindAllWebsites().First(w=>w.WebsiteType == WebsiteType.Pick),
                Data = HtmlPageGenerator.GetPicksPage()
            };
            return webpage;
        }

        public Webpage GetEmptyWebpage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "url",
                Website = new WebsiteFinder(context).FindAllWebsites().First(w => w.WebsiteType == WebsiteType.Result),
                Data = "test data"
            };
            return webpage;
        }
    }
}
