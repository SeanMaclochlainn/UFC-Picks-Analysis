using FightData.TestData;

namespace FightData.Domain.Test
{
    public class TestDomain
    {
        protected FightPicksContext context;

        public TestDomain()
        {
            context = new TestDatabase().Context;
        }
    }
}
