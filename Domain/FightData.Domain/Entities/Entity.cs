
namespace FightData.Domain.Entities
{
    public class Entity
    {
        protected FightPicksContext context;

        public Entity() { }

        public Entity(FightPicksContext context)
        {
            this.context = context;
        }
    }
}
