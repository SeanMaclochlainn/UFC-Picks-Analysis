using System;
using System.Collections.Generic;

namespace FightData.Models.DataModels
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DomainName { get; set; }
        public List<Webpage> Webpages { get; set; }
    }
}
