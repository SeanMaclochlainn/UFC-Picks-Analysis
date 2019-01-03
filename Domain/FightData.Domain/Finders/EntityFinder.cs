namespace FightData.Domain.Finders
{
    public class EntityFinder : DataFinder
    {
        public EntityFinder(FightPicksContext context) : base(context)
        {
            AnalystFinder = new AnalystFinder(context);
            ExhibitionFinder = new ExhibitionFinder(context);
            FighterFinder = new FighterFinder(context);
            FightFinder = new FightFinder(context);
            PickFinder = new PickFinder(context);
            WebpageFinder = new WebpageFinder(context);
            WebsiteFinder = new WebsiteFinder(context);
            OddsFinder = new OddsFinder(context);
            PicksPageConfigurationFinder = new PicksPageConfigurationFinder(context);
        }

        public AnalystFinder AnalystFinder { get; private set; }
        public ExhibitionFinder ExhibitionFinder { get; private set; }
        public FighterFinder FighterFinder { get; private set; }
        public FightFinder FightFinder { get; private set; }
        public PickFinder PickFinder { get; private set; }
        public WebpageFinder WebpageFinder { get; private set; }
        public WebsiteFinder WebsiteFinder { get; private set; }
        public OddsFinder OddsFinder { get; private set; }
        public PicksPageConfigurationFinder PicksPageConfigurationFinder { get; private set; }
    }
}
