using System;
using System.Collections.Generic;

namespace FightData.Models
{
    public class CardType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fight> Fights { get; set; }
    }
}
