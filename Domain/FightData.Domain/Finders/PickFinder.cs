using FightData.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Finders
{
    public class PickFinder : DataFinder
    {
        public PickFinder(FightPicksContext context) : base(context) { }

        public static List<Pick> FindPicks(Exhibition exhibition)
        {
            List<Pick> picks = new List<Pick>();
            foreach(Fight fight in exhibition.Fights)
                picks.AddRange(fight.Picks);
            return picks;
        }

        public static List<Pick> FindPicks(List<Exhibition> exhibitions)
        {
            List<Pick> picks = new List<Pick>();
            foreach (Exhibition exhibition in exhibitions)
                picks.AddRange(FindPicks(exhibition));
            return picks;
        }

        public static List<Pick> FindAnalystsPicks(Analyst analyst, List<Exhibition> exhibitions)
        {
            List<Pick> picks = new List<Pick>();
            foreach (Exhibition exhibition in exhibitions)
                picks.AddRange(FindAnalystsPicks(analyst, exhibition));
            return picks;
        }

        public static List<Pick> FindAnalystsPicks(Analyst analyst, Exhibition exhibition)
        {
            List<Pick> picks = FindPicks(exhibition);
            return picks.Where(p => p.Analyst.Id == analyst.Id).ToList();
        }

        public FinderResult<Pick> FindPick(Analyst analyst, Fight fight)
        {
            return new FinderResult<Pick>(fight.Picks.SingleOrDefault(p => p.Analyst.Id == analyst.Id));
        }
    }
}
