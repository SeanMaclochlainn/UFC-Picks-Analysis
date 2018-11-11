using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;
using System.Linq;

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
            analyst.Website = new WebsiteFinder(context).GetAllWebsites().First(w => w.WebsiteType == WebsiteType.Result);
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
