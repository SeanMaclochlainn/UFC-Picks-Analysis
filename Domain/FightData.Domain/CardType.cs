using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain
{
    public class CardType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fight> Fights { get; set; }
    }
}
