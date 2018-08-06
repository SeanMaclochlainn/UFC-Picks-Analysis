using FightData.Domain.Entities;

namespace FightData.Domain
{
    public class FighterAdder
    {
        private FightPicksContext context;

        public FighterAdder(FightPicksContext context)
        {
            this.context = context;
        }

        public void AddFighter(string name)
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames(name);
            fighter.Add();
        }
    }
}
