using FightData.Domain.Builders;
using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain
{
    public class FighterUpdater
    {
        private FightPicksContext context;
        private FighterFinder fighterFinder;
        private FighterBuilder fighterBuilder;

        public FighterUpdater(FightPicksContext context)
        {
            this.context = context;
            fighterFinder = new FighterFinder(context);
            fighterBuilder = new FighterBuilder(context);
        }

        public void AddFighter(string name)
        {
            Fighter fighter = fighterBuilder.GenerateFighter(name).Build();   
            Add(fighter);
        }

        public void Add(Fighter fighter)
        {
            context.Fighters.Add(fighter);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            context.Fighters.RemoveRange(fighterFinder.GetAllFighters());
            context.SaveChanges();
        }
    }
}
