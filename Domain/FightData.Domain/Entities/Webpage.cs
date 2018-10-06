using System.Xml.Linq;

namespace FightData.Domain.Entities
{
    public class Webpage : Entity
    {
        public Webpage() { }

        public Webpage(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public Exhibition Exhibition { get; set; }
        public string Data { get; set; }

        public void Add()
        {
            Context.Webpages.Add(this);
            Context.SaveChanges();
        }

        public XDocument GetHtml()
        {
            return XDocument.Parse(Data);
        }
    }
}
