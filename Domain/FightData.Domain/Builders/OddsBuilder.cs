using FightData.Domain.Entities;

namespace FightData.Domain.Builders
{
    public class OddsBuilder
    {
        private FightPicksContext context;

        public OddsBuilder(FightPicksContext context)
        {
            this.context = context;
        }

        public FightPicksContext Context { get; private set; }
        public decimal Value { get; private set; }
        public Fighter Fighter { get; private set; }
        public Fight Fight { get; private set; }

        public OddsBuilder GenerateOdd(decimal value, Fighter fighter, Fight fight)
        {
            Value = value;
            Fighter = fighter;
            Fight = fight;
            return this;
        }

        public Odd Build()
        {
            return new Odd(this);
        }
    }
}
