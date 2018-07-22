using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FighterFinder : DataFinder
    {
        public FighterFinder(FightPicksContext context) : base(context) { }

        public FinderResult<Fighter> FindFighter(string name)
        {
            Fighter fighter = context.Fighters.FirstOrDefault(f => f.FullName == name);
            return new FinderResult<Fighter>(fighter);
        }
    }
}
