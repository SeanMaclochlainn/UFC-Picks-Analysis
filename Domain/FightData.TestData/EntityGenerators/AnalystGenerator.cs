using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class AnalystGenerator
    {
        private FightPicksContext context;

        public AnalystGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Analyst GetPopulatedAnalyst()
        {
            Analyst analyst = new Analyst(context);
            analyst.AltNames = new List<AnalystAltName>() { GetAltName(analyst) };
            analyst.Name = "Mike Bohn";
            analyst.Website = new WebsiteGenerator(context).GetResultsPageWebsite();
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
