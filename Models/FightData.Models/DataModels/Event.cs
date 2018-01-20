using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public List<Fight> Fights { get; set; }
        public List<Webpage> Webpages { get; set; }
    }
}
