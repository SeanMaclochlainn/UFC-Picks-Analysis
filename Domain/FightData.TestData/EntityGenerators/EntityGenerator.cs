
using FightData.Domain;

namespace FightData.TestData.EntityGenerators
{
    public class EntityGenerator
    {
        protected FightPicksContext context;

        public EntityGenerator(FightPicksContext context)
        {
            this.context = context;
        }
    }
}
