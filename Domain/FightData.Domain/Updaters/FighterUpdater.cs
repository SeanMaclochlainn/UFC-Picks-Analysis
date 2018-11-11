using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain
{
    public class FighterUpdater
    {
        private FightPicksContext context;
        private FighterFinder fighterFinder;

        public FighterUpdater(FightPicksContext context)
        {
            this.context = context;
            fighterFinder = new FighterFinder(context);
        }

        public void AddFighter(string name)
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames(name);
            fighter.Add();
        }

        public void DeleteAll()
        {
            context.Fighters.RemoveRange(fighterFinder.GetAllFighters());
            context.SaveChanges();
        }
    }
}
