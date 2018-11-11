using FightData.Domain.Entities;
using FightData.TestData;
using FightData.TestData.EntityGenerators;

namespace FightData.Domain.Test
{
    public class TestDataLayer
    {
        protected FightPicksContext context;
        protected EntityGenerator entityGenerator;

        public TestDataLayer()
        {
            context = new TestDatabase().Context;
            entityGenerator = new EntityGenerator(context);
            new TestDatabaseSeeder(context).Seed();
        }
    }
}
