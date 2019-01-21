using FightData.Domain.Builders;
using FightData.Domain.Updaters;

namespace FightData.Domain.Entities
{
    public class Webpage : Entity
    {
        public Webpage(FightPicksContext context) : base(context) { }

        public Webpage(WebpageBuilder webpageBuilder) : this(webpageBuilder.Context)
        {
            Website = webpageBuilder.Website;
            Exhibition = webpageBuilder.Exhibition;
            Data = webpageBuilder.Data;
            Parsed = webpageBuilder.Parsed;
            Url = webpageBuilder.Url;
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public Exhibition Exhibition { get; set; }
        public string Data { get; set; }
        public bool Parsed { get; set; }

        public void DownloadData(Client client)
        {
            Data = client.Download(Url);
        }
    }
}
