using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class Pick
    {
        public int Id { get; set; }
        public Analyst Analyst { get; set; }
        public Fight Fight { get; set; }
        public Fighter FighterPick { get; set; }

        public bool Correct()
        {
            if (Fight.Winner == FighterPick)
                return true;
            else
                return false;
        }
    }
}
