﻿using System.Collections.Generic;

namespace FightData.Domain.Entities
{
    public class Website
    {
        public int Id { get; set; }
        public WebsiteName WebsiteName { get; set; }
        public List<Webpage> Webpages { get; set; }
        public WebsiteType WebsiteType { get; set; }
    }
}
