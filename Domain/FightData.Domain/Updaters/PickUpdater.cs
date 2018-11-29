using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.Domain.Updaters
{
    public class PickUpdater
    {
        private FightPicksContext context;
        private Pick pick;

        public PickUpdater(FightPicksContext context)
        {
            this.context = context;
        }

        public void AddPicks(List<Pick> picks)
        {
            foreach(Pick pick in picks)
            {
                AddPick(pick);
            }
        }

        public void AddPick(Pick pick)
        {
            this.pick = pick;
            if (AreEntitiesValid())
            {
                context.Picks.Add(pick);
                context.SaveChanges();
            }
        }

        private bool AreEntitiesValid()
        {
            return pick.Analyst != null && pick.Fight!= null && pick.Fighter != null;
        }
    }
}
