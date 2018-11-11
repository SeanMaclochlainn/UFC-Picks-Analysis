using FightData.Domain.Entities;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class PickFinder : DataFinder
    {
        public PickFinder(FightPicksContext context) : base(context) { }

        public FinderResult<Pick> FindPick(Analyst analyst, Fight fight)
        {
            return new FinderResult<Pick>(fight.Picks.SingleOrDefault(p => p.Analyst.Id == analyst.Id));
        }
    }
}
