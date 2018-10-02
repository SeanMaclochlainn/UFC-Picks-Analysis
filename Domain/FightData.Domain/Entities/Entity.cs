
namespace FightData.Domain.Entities
{
    public class Entity
    {
        public Entity() { }

        public Entity(FightPicksContext context)
        {
            Context = context;
        }

        public FightPicksContext Context { get; set; }
    }
}
