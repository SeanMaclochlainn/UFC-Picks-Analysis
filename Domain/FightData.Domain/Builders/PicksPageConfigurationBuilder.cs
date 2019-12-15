using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Builders
{
    public class PicksPageConfigurationBuilder
    {
        private EntityFinder entityFinder;

        public PicksPageConfigurationBuilder(FightPicksContext context)
        {
            Context = context;
            entityFinder = new EntityFinder(context);
        }

        public FightPicksContext Context { get; private set; }
        public PicksPageRowType PicksPageRowType { get; private set; }
        public string AnalystXpath { get; private set; }
        public string AnalystRegex { get; private set; }
        public string FighterXpath { get; private set; }
        public string FighterRegex { get; private set; }
        public Website Website { get; private set; }

        public PicksPageConfigurationBuilder GeneratePicksPageConfiguration(PicksPageRowType picksPageRowType, string AnalystXpath, string AnalystRegex, string FighterXpath, string FighterRegex, Website website)
        {
            return this;
        }

        public PicksPageConfigurationBuilder GenerateSampleMmaJunkiePicksPageConfiguration()
        {
            AnalystXpath = "//table//tr[{row-incrementer}]/td[1]/strong";
            FighterXpath = "//table//tr[{row-incrementer}]/td[{column-incrementer}+1]";
            PicksPageRowType = PicksPageRowType.SingleAnalystMultipleFighters;
            Website = entityFinder.WebsiteFinder.FindWebsite(WebsiteName.MMAJunkie);
            return this;
        }

        public PicksPageConfigurationBuilder GenerateSampleBloodyElbowPicksPageConfiguration()
        {
            AnalystXpath = "(//text()[starts-with(normalize-space(),'Staff picking')])[{row-incrementer}]";
            AnalystRegex = @"(?:[:|,]\s(\w+)){{column-incrementer}}";
            FighterXpath = "(//text()[starts-with(normalize-space(),'Staff picking')])[{row-incrementer}]";
            FighterRegex = @"(?<=Staff picking )[A-Za-z'\s\-]+(?=:)";
            PicksPageRowType = PicksPageRowType.SingleFighterMultipleAnalysts;
            Website = entityFinder.WebsiteFinder.FindWebsite(WebsiteName.BloodyElbow);
            return this;
        }

        public PicksPageConfiguration Build()
        {
            return new PicksPageConfiguration(this);
        }
    }
}
