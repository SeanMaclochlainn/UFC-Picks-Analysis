using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain.Finders
{
    public class FighterFinder : DataFinder
    {
        public FighterFinder() { }

        private FighterFinder(FightPicksContext context) : base(context) { }

        public static FighterFinder WithCustomContext(FightPicksContext context)
        {
            return new FighterFinder(context);
        }

        public bool Found { get; private set; }

        public Fighter Fighter { get; private set; }

        public void FindFighter(string name)
        {
            Fighter = context.Fighters.FirstOrDefault(f => f.FullName == name);
            Found = IsFound();
        }

        private bool IsFound()
        {
            return !(Fighter == null);
        }
    }
}
