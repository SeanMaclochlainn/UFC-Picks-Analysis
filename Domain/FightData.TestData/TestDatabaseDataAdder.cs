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

        public void AddEvent()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            context.Add(ufcEvent);
            context.SaveChanges();
        }

        public void AddPick()
        {
            context.Add(pickGenerator.GetPick());
            context.SaveChanges();
        }

        public void AddRegularFighter()
        {
            context.Add(fighterGenerator.GetFighter());
            context.SaveChanges();
        }
    }
}
