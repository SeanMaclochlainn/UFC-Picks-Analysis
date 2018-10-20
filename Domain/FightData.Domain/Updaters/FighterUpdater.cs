using FightData.Domain.Entities;

namespace FightData.Domain
{
    public class FighterUpdater
    {
        private FightPicksContext context;

        public FighterUpdater(FightPicksContext context)
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
