using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class ExhibitionFinder : DataFinder
    {
        public ExhibitionFinder(FightPicksContext context) : base(context) { }

        public List<Exhibition> FindAllExhibitions()
        {
            return context.Exhibitions.ToList();
        }
    }
}
