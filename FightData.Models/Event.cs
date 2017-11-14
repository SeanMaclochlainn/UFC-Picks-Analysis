using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public List<Fight> Fights { get; set; }
    }
}
