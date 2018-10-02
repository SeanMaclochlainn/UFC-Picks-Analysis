
namespace FightData.Domain.Finders
{
    public class DataFinder
    {
        protected FightPicksContext context;

        public DataFinder(FightPicksContext context)
        {
            this.context = context;
        }
    }
}
