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

        public Analyst GetAnalyst()
        {
            Analyst analyst = new Analyst();
            analyst.AltNames = new List<AnalystAltName>() { GetAnalystAltName(analyst) };
            analyst.Name = "test analyst";
            analyst.Picks = new List<Pick>();
            analyst.Website = websiteGenerator.GetResultsWebsite();
            return analyst;
        }

        public static AnalystAltName GetAnalystAltName(Analyst analyst)
        {
            AnalystAltName analystAltName = new AnalystAltName();
            analystAltName.Name = "test alt name";
            analystAltName.Analyst = analyst;
            return analystAltName;
        }
    }
}
