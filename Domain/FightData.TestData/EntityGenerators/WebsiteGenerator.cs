using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class WebsiteGenerator
    {
        private FightPicksContext context;

        public WebsiteGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Website GetResultsPageWebsite()
        {
            return new Website { DomainName = "wikipedia", WebsiteName = WebsiteName.Wikipedia };
        }

        public Website GetPicksPageWebsite()
        {
            return new Website { DomainName = "mmajunkie", WebsiteName = WebsiteName.MMAJunkie };
        }
    }
}
