using FightData.Domain.Finders;
using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDataLayer
    {
        protected FightPicksContext context;
        protected EntityFinder entityFinder;

        public TestDataLayer()
        {
            context = new TestDatabase().Context;
            entityFinder = new EntityFinder(context);
            new TestDatabaseSeeder(context).Seed();
        }
    }
}
