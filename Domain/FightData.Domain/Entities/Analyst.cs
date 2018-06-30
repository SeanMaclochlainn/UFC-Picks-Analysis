using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Entities
{
    public class Analyst
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Website Website { get; set; }
        public List<Pick> Picks { get; set; }
        public List<AnalystAltName> AltNames { get; set; }

        public string GetNameOfPick(Fight fight)
        {
            Pick pick = fight.Picks.FirstOrDefault(p => p.Analyst == this);
            if (pick != null)
                return pick.FighterPick.LastName;
            else
                return "";
        }

        public bool HasPick(Fight fight)
        {
            if (fight.Picks != null)
            {
                return fight.Picks.Any(p => p.Analyst == this);
            }
            else
                return false;
        }

        public Pick GetPick(Fight fight)
        {
            Pick pick = fight.Picks.FirstOrDefault(p => p.Analyst == this);
            return pick;
        }
    }
}
