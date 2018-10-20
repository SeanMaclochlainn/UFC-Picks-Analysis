using FightData.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class AnalystFinder : DataFinder
    {
        public AnalystFinder(FightPicksContext context) : base(context) { }

        public FinderResult<Analyst> FindAnalyst(string name)
        {
            Analyst analyst = context.Analysts.FirstOrDefault(a => a.Name == name);
            return new FinderResult<Analyst>(analyst);
        }

        public List<Analyst> GetAllAnalysts()
        {
            return context.Analysts.ToList();
        }
    }
}

