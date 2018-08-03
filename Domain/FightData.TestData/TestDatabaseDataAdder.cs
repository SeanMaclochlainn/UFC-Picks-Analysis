using FightData.Domain;
using FightData.Domain.Entities;
using FightData.TestData.EntityGenerators;

namespace FightData.TestData
{
    public class TestDatabaseDataAdder
    {
        private FightPicksContext context;
        private UfcEventGenerator ufcEventGenerator;
        private PickGenerator pickGenerator;
        private FighterGenerator fighterGenerator;

        public TestDatabaseDataAdder(FightPicksContext context)
        {
            this.context = context;
            ufcEventGenerator = new UfcEventGenerator(context);
            pickGenerator = new PickGenerator(context);
            fighterGenerator = new FighterGenerator(context);
        }

        public void AddPopulatedEvent()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            context.Add(ufcEvent);
            context.SaveChanges();
        }

        public void AddPopulatedPick()
        {
            context.Add(pickGenerator.GetPopulatedPick());
            context.SaveChanges();
        }

        public void AddPopulatedFighter()
        {
            context.Add(fighterGenerator.GetPopulatedFighter());
            context.SaveChanges();
        }
    }
}
