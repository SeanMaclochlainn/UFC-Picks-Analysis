using System.Xml.Linq;

namespace FightData.Domain.Entities
{
    public class Webpage : Entity
    {
        public Webpage() : base(new FightPicksContext()) { }
        public Webpage(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public Exhibition Exhibition { get; set; }
        public string Data { get; set; }

        public XDocument GetHtml()
        {
            return XDocument.Parse(Data);
        }
    }
}
