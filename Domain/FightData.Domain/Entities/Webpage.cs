using FightData.Domain.Updaters;
using System.Xml.Linq;

namespace FightData.Domain.Entities
{
    public class Webpage : Entity
    {
        public Webpage() { }

        public Webpage(FightPicksContext context) : base(context) { }

        public Webpage(FightPicksContext context, string url, Website website) : base(context)
        {
            Url = url;
            Website = website;
        }

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

        public void DownloadData(Client client)
        {
            Data = client.Download(Url);
        }
    }
}
