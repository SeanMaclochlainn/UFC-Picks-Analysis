﻿
using FightData.Domain.Entities;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class AnalystFinder : DataFinder
    {
        public AnalystFinder(FightPicksContext context) : base(context) { }

        private FinderResult<Analyst> FindAnalyst(string name)
        {
            Analyst analyst = context.Analysts.FirstOrDefault(a => a.Name == name);
            return new FinderResult<Analyst>(analyst);
        }
    }
}
