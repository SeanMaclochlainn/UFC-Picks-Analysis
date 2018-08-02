
namespace FightData.Domain.Entities
{
    public class Webpage : Entity
    {
        private Webpage() { }

        public Webpage(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public UfcEvent Event { get; set; }
        public string Data { get; set; }
        public WebpageType WebpageType { get; set; }

        //public void UpdateWebpage(string data, string url)
        //{
        //    this.Data = data;
        //    this.Url = url;
        //    context.SaveChanges();
        //}

        //public void AddWebpage()
        //{
        //    context.Webpages.Add(this);
        //    context.SaveChanges();
        //}
    }
}
