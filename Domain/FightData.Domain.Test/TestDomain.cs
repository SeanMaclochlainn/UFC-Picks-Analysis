using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDomain
    {
        protected FightPicksContext context;
        protected TestDatabaseDataAdder databaseDataGenerator;
        protected TestEntityGenerator entityDataGenerator;

        public TestDomain()
        {
            context = new TestDatabase().Context;
            databaseDataGenerator = new TestDatabaseDataAdder(context);
            entityDataGenerator = new TestEntityGenerator(context);
        }
    }
}
