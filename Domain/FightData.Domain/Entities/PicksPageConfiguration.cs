using FightData.Domain.Builders;

namespace FightData.Domain.Entities
{
    public class PicksPageConfiguration : Entity
    {
        public PicksPageConfiguration(FightPicksContext context) : base(context) { }

        public PicksPageConfiguration(PicksPageConfigurationBuilder picksPageConfigurationBuilder) : this(picksPageConfigurationBuilder.Context)
        {
            PicksPageRowType = picksPageConfigurationBuilder.PicksPageRowType;
            AnalystXpath = picksPageConfigurationBuilder.AnalystXpath;
            AnalystRegex = picksPageConfigurationBuilder.AnalystRegex;
            FighterXpath = picksPageConfigurationBuilder.FighterXpath;
            FighterRegex = picksPageConfigurationBuilder.FighterRegex;
            Website = picksPageConfigurationBuilder.Website;
        }

        public int Id { get; set; }
        public PicksPageRowType PicksPageRowType { get; set; }
        public string AnalystXpath { get; set; }
        public string AnalystRegex { get; set; }
        public string FighterXpath { get; set; }
        public string FighterRegex { get; set; }
        public int WebsiteId { get; set; }
        public Website Website { get; set; }
    }
}
