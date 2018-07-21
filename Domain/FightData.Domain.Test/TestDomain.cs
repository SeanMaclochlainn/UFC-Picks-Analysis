using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDomain
    {
        private TestDatabase database;
        protected FightPicksContext context;
        protected TestDatabaseDataAdder databaseDataGenerator;
        protected TestEntityGenerator entityDataGenerator;

        public TestDomain()
        {
            database = new TestDatabase();
            context = database.Context;
            databaseDataGenerator = new TestDatabaseDataAdder(context);
            entityDataGenerator = new TestEntityGenerator(context);
        }
    }
}
