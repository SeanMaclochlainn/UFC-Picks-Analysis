using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Domain.Updaters
{
    public class PicksPageConfigurationUpdater
    {
        private FightPicksContext context;

        public PicksPageConfigurationUpdater(FightPicksContext context)
        {
            this.context = context;
        }

        public void Add(PicksPageConfiguration picksPageConfiguration)
        {
            context.PicksPageConfigurations.Add(picksPageConfiguration);
            context.SaveChanges();
        }

        public void Add(List<PicksPageConfiguration> picksPageConfigurations)
        {
            foreach (PicksPageConfiguration picksPageConfiguration in picksPageConfigurations)
                context.Add(picksPageConfiguration);
            context.SaveChanges();
        }
    }
}
