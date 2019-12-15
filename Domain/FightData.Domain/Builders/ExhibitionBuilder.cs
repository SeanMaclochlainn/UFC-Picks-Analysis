using FightData.Domain.Entities;

namespace FightData.Domain.Builders
{
    public class ExhibitionBuilder
    {
        public ExhibitionBuilder(FightPicksContext context)
        {
            Context = context;
        }

        public FightPicksContext Context { get; private set; }
        public string Name { get; private set; }

        public ExhibitionBuilder GenerateExhibition(string name)
        {
            Name = name;
            return this;
        }

        public Exhibition Build()
        {
            return new Exhibition(this);
        }
    }
}
