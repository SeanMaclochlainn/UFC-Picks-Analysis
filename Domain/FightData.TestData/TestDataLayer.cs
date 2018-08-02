using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDataLayer
    {
        protected FightPicksContext context;

        public TestDataLayer()
        {
            context = new TestDatabase().Context;
        }
    }
}
