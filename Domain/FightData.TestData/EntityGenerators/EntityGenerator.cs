
using FightData.Domain;

namespace FightData.TestData.EntityGenerators
{
    public class EntityGenerator
    {
        public EntityGenerator(FightPicksContext context)
        {
            AnalystGenerator = new AnalystGenerator(context);
            FighterGenerator = new FighterGenerator(context);
            FightGenerator = new FightGenerator(context);
            PickGenerator = new PickGenerator(context);
            ExhibitionGenerator = new ExhibitionGenerator(context);
            WebpageGenerator = new WebpageGenerator(context);
            WebsiteGenerator = new WebsiteGenerator(context);
        }

        public AnalystGenerator AnalystGenerator { get; private set; }
        public FighterGenerator FighterGenerator { get; private set; }
        public FightGenerator FightGenerator { get; private set; }
        public PickGenerator PickGenerator { get; private set; }
        public ExhibitionGenerator ExhibitionGenerator { get; private set; }
        public WebpageGenerator WebpageGenerator { get; private set; }
        public WebsiteGenerator WebsiteGenerator { get; private set; }
        
    }
}
