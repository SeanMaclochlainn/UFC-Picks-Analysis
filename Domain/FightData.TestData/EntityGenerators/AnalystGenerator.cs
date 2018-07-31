using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class AnalystGenerator : EntityGenerator
    {
        private WebsiteGenerator websiteGenerator;

        public AnalystGenerator(FightPicksContext context) : base(context)
        {
            websiteGenerator = new WebsiteGenerator(context);
        }

        public Analyst GetPopulatedAnalyst()
        {
            Analyst analyst = new Analyst();
            analyst.AltNames = new List<AnalystAltName>() { GetAltName(analyst) };
            analyst.Name = "test analyst";
            analyst.Website = websiteGenerator.GetResultsPageWebsite();
            return analyst;
        }

        private static AnalystAltName GetAltName(Analyst analyst)
        {
            AnalystAltName analystAltName = new AnalystAltName();
            analystAltName.Name = "test alt name";
            analystAltName.Analyst = analyst;
            return analystAltName;
        }
    }
}
