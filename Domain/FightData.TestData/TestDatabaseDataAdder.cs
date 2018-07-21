using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData
{
    public class TestDatabaseDataAdder
    {
        private FightPicksContext context;
        private TestEntityGenerator entityDataGenerator;

        public TestDatabaseDataAdder()
        {
            context = new TestDatabase().Context;
            entityDataGenerator = new TestEntityGenerator(context);
        }
        public TestDatabaseDataAdder(FightPicksContext context)
        {
            this.context = context;
            entityDataGenerator = new TestEntityGenerator(context);
        }

        public void AddEvent()
        {
            UfcEvent ufcEvent = entityDataGenerator.GetUfcEvent();
            context.Add(ufcEvent);
            context.SaveChanges();
        }

        public void AddPick()
        {
            context.Add(entityDataGenerator.GetPick());
            context.SaveChanges();
        }

        public void AddRegularFighter()
        {
            context.Add(entityDataGenerator.GetFighter());
            context.SaveChanges();
        }
    }
}
