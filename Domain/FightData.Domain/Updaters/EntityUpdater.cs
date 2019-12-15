namespace FightData.Domain.Updaters
{
    public class EntityUpdater
    {
        private FightPicksContext context;

        public EntityUpdater(FightPicksContext context)
        {
            this.context = context;
            AnalystUpdater = new AnalystUpdater(context);
            ExhibitionUpdater = new ExhibitionUpdater(context);
            FighterUpdater = new FighterUpdater(context);
            FightUpdater = new FightUpdater(context);
            OddUpdater = new OddUpdater(context);
            PicksPageConfigurationUpdater = new PicksPageConfigurationUpdater(context);
            PickUpdater = new PickUpdater(context);
            WebpageUpdater = new WebpageUpdater(context);
            WebsiteUpdater = new WebsiteUpdater(context);
        }

        public AnalystUpdater AnalystUpdater { get; private set; }
        public ExhibitionUpdater ExhibitionUpdater { get; private set; }
        public FighterUpdater FighterUpdater { get; private set; }
        public FightUpdater FightUpdater { get; private set; }
        public OddUpdater OddUpdater { get; private set; }
        public PicksPageConfigurationUpdater PicksPageConfigurationUpdater { get; private set; }
        public PickUpdater PickUpdater { get; private set; }
        public WebpageUpdater WebpageUpdater { get; private set; }
        public WebsiteUpdater WebsiteUpdater { get; private set; }
    }
}
