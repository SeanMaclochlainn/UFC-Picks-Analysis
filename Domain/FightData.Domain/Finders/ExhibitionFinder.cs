using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FightData.Domain.Finders
{
    public class ExhibitionFinder : DataFinder
    {
        public ExhibitionFinder(FightPicksContext context) : base(context) { }

        public List<Exhibition> FindNewestToOldest()
        {
            return FindAllExhibitions().OrderByDescending(e => e.Date).ToList();
        }

        public List<Exhibition> FindOldestToNewest()
        {
            return FindAllExhibitions().OrderBy(e => e.Date).ToList();
        }

        public List<Exhibition> FindAllExhibitions()
        {
            return context.Exhibitions.Include(e => e.Webpages)
                .ThenInclude(w => w.Website)
                .Include(e => e.Fights)
                .ThenInclude(f => f.Winner)
                .Include(e => e.Fights)
                .ThenInclude(f => f.Loser)
                .Include(e => e.Fights)
                .ThenInclude(f => f.Picks)
                .ThenInclude(p => p.Analyst)
                .Include(e => e.Fights)
                .ThenInclude(f => f.Odds)
                .ToList();
        }

        public Exhibition FindExhibition(int id)
        {
            return FindAllExhibitions().Single(e => e.Id == id);
        }

        public Exhibition FindExhibition(string name)
        {
            return context.Exhibitions.Single(e => e.Name == name);
        }
    }
}
