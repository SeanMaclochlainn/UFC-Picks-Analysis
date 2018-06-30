using FightData.Domain;
using FightData.Domain.Entities;

namespace FightData.TestData
{
    public class DatabaseDataAdder
    {
        private FightPicksContext context;
        private EntityDataGenerator entityDataGenerator;

        public DatabaseDataAdder(FightPicksContext context)
        {
            this.context = context;
            entityDataGenerator = new EntityDataGenerator(context);
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
