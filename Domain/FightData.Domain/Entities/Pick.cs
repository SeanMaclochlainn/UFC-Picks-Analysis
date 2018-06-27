using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Entities
{
    public class Pick
    {
        //TD rename pick to Analyst pick
        public int Id { get; set; }
        public Analyst Analyst { get; set; }
        public Fight Fight { get; set; }
        public Fighter FighterPick { get; set; }

        public UfcEvent Event
        {
            get
            {
                return Fight?.UfcEvent;
            }
        }

        public bool Correct()
        {
            if (Fight.Winner == FighterPick)
                return true;
            else
                return false;
        }
    }
}
