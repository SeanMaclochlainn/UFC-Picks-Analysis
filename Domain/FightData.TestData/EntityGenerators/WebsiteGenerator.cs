using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class WebsiteGenerator : EntityGenerator
    {
        public WebsiteGenerator(FightPicksContext context) : base(context) { }

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
