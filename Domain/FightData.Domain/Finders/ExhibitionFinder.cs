using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FightData.Domain.Finders
{
    public class ExhibitionFinder : DataFinder
    {
        public ExhibitionFinder(FightPicksContext context) : base(context) { }

        public List<Exhibition> FindAllExhibitions()
        {
            return context.Exhibitions.Include(e => e.Webpages)
                .ThenInclude(w => w.Website)
                .Include(e => e.Fights)
                .ThenInclude(f => f.Winner)
                .Include(e => e.Fights)
                .ThenInclude(f => f.Loser)
                .ToList();
        }

        public Exhibition FindExhibition(int id)
        {
            return context.Exhibitions.Include(e => e.Webpages).ThenInclude(w => w.Website).Single(e => e.Id == id);
        }
    }
}
