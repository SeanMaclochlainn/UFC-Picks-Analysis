using FightData.Domain.Entities;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class PicksPageConfigurationFinder : DataFinder 
    {
        public PicksPageConfigurationFinder(FightPicksContext context) : base(context) { }

        public PicksPageConfiguration FindConfiguration(Website website)
        {
            return context.PicksPageConfigurations.Single(ppc => ppc.Website == website);
        }
    }
}
