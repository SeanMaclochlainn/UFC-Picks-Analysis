using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Models.ViewModels
{
    public class FightEventVM
    {
        public List<Event> FightEvents { get; set; }
        public List<Analyst> Analysts { get; set; }
    }
}
