namespace FightData.Domain.Entities
{
    public class PicksPageConfiguration : Entity
    {
        public PicksPageConfiguration(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public PicksPageRowType PicksPageRowType { get; set; }
        public string AnalystXpath { get; set; }
        public string FighterXpath { get; set; }
        public int WebsiteId { get; set; }
        public Website Website { get; set; }
    }
}
