using System;
using System.Collections.Generic;

namespace FightData.Models
{
    public class Fighter
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<FighterAltName> AltNames { get; set; }
    }
}
