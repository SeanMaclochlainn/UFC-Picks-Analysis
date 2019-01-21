using FightData.Domain.Entities;

namespace FightData.Domain.Updaters
{
    public class AnalystUpdater
    {
        private FightPicksContext context;

        public AnalystUpdater(FightPicksContext context)
        {
            this.context = context;
        }

        public void Add(Analyst analyst)
        {
            context.Add(analyst);
            context.SaveChanges();
        }
    }
}
