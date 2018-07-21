using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDomain
    {
        private TestDatabase database;
        protected FightPicksContext context;
        protected DatabaseDataAdder databaseDataGenerator;
        protected EntityDataGenerator entityDataGenerator;

        public TestDomain()
        {
            database = new TestDatabase();
            context = database.Context;
            databaseDataGenerator = new DatabaseDataAdder(context);
            entityDataGenerator = new EntityDataGenerator(context);
        }
    }
}
