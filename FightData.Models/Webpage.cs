using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Models
{
    public class Webpage
    {
        public string URL { get; set; }
        public Website Website { get; set; }
        public Event UFCEvent { get; set; }
        public string Data { get; set; }
    }
}
