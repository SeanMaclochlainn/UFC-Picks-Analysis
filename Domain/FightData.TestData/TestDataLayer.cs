using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDataLayer
    {
        protected FightPicksContext context;
        protected EntityFinder entityFinder;
        protected EntityUpdater entityUpdater;

        public TestDataLayer()
        {
            context = new TestDatabase().Context;
            entityFinder = new EntityFinder(context);
            entityUpdater = new EntityUpdater(context);
        }
    }
}
