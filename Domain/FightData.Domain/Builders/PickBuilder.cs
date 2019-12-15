using FightData.Domain.Entities;

namespace FightData.Domain.Builders
{
    public class PickBuilder
    {

        public PickBuilder(FightPicksContext context)
        {
            Context = context;
        }

        public PickBuilder GeneratePick(Analyst analyst, Fight fight, Fighter fighter)
        {
            Analyst = analyst;
            Fight = fight;
            Fighter = fighter;
            return this;
        }

        public Pick Build()
        {
            return new Pick(this);
        }

        public FightPicksContext Context { get; private set; }
        public Analyst Analyst { get; private set; }
        public Fight Fight { get; private set; }
        public Fighter Fighter { get; private set; }

    }
}
