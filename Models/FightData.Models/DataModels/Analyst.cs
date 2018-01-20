using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class Analyst
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pick> Picks { get; set; }
    }
}
