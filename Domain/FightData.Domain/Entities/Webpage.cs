using System.Xml.Linq;

namespace FightData.Domain.Entities
{
    public class Webpage : Entity
    {
        public Webpage(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public UfcEvent Event { get; set; }
        public string Data { get; set; }
        public WebpageType WebpageType { get; set; }

        public XDocument GetHtml()
        {
            return XDocument.Parse(Data);
        }
    }
}
