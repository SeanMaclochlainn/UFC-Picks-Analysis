using FightData.Domain.Entities;

namespace FightData.Domain.Builders
{
    public class FighterBuilder
    {
        public FighterBuilder(FightPicksContext context)
        {
            Context = context;
        }

        public FightPicksContext Context { get; private set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }

        public FighterBuilder GenerateFighter(string name)
        {
            NameParser nameParser = new NameParser(name);
            FullName = nameParser.GetFullName();
            FirstName = nameParser.GetFirstName();
            LastName = nameParser.GetLastName();
            MiddleName = nameParser.GetMiddleNames();
            return this;
        }

        public Fighter Build()
        {
            return new Fighter(this);
        }
    }
}
