using System.Collections.Generic;

namespace FightData.Domain.Entities
{
    public class Fighter : Entity
    {
        public Fighter(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public List<FighterAltName> FighterAltNames { get; set; }
        public List<Fight> Wins { get; set; }
        public List<Fight> Losses { get; set; }
        public List<Pick> Picks { get; set; }
        public List<Odd> Odds { get; set; }

        public static Fighter GenerateFighter(string name, FightPicksContext context)
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames(name);
            return fighter;
        }

        public void Add()
        {
            Context.Fighters.Add(this);
            Context.SaveChanges();
        }

        public void PopulateNames(string nameText)
        {
            NameParser nameParser = new NameParser(nameText);
            FullName = nameParser.GetFullName();
            FirstName = nameParser.GetFirstName();
            LastName = nameParser.GetLastName();
            MiddleName = nameParser.GetMiddleNames();
        }


    }
}
