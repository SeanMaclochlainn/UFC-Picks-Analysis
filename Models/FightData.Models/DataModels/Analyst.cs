using System;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Models.DataModels
{
    public class Analyst
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pick> Picks { get; set; }
        
        public string GetNameOfPick(Fight fight)
        {
            Pick pick = fight.Picks.FirstOrDefault(p => p.Analyst == this);
            if (pick != null)
                return pick.FighterPick.LastName;
            else
                return "";
        }
    }
}
