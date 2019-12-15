using System.Collections.Generic;

namespace FightData.Domain.Entities
{
    public class Website : Entity
    {
        public Website() { } 
        public Website(FightPicksContext context) : base(context) { }
        public int Id { get; set; }
        public WebsiteName WebsiteName { get; set; }
        public List<Webpage> Webpages { get; set; }
        public WebsiteType WebsiteType { get; set; }
        public PicksPageConfiguration PicksPageConfiguration { get; set; }
    }
}
