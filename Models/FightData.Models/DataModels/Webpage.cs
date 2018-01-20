using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class Webpage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public Event Event { get; set; }
        public string Data { get; set; }
    }
}
