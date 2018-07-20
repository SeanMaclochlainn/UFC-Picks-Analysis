
using FightData.Domain.Entities;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class AnalystFinder : DataFinder
    {
        private string name;

        public AnalystFinder(string name) : this(name, new FightPicksContext()) { }

        public AnalystFinder(string name, FightPicksContext context) : base(context)
        {
            this.name = name;
            Analyst = FindAnalyst();
            AnalystExists = DoesAnalystExist();
        }

        public Analyst Analyst { get; set; }
        public bool AnalystExists { get; set; }

        private Analyst FindAnalyst()
        {
            return context.Analysts.FirstOrDefault(a => a.Name == name);
        }

        private bool DoesAnalystExist()
        {
            return !(Analyst == null);
        }
    }
}

