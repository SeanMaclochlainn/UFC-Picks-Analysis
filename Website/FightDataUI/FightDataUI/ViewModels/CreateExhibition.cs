using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightDataUI.ViewModels
{
    public class CreateExhibition
    {
        public CreateExhibition()
        {
            Exhibition = new Exhibition();
        }

        public CreateExhibition(Exhibition exhibition)
        {
            Exhibition = exhibition;
        }

        public Exhibition Exhibition { get; private set; }

        public void LoadViewData(FightPicksContext context)
        {
            AddAllWebsiteWebpages(context);
        }

        public void ProcessViewData(FightPicksContext context, Client client)
        {
            DownloadWebpageData(client);
            AddExhibition(context);
        }

        private void AddAllWebsiteWebpages(FightPicksContext context)
        {
            foreach (Website website in new WebsiteFinder(context).FindAllWebsites())
                Exhibition.Webpages.Add(new Webpage(context) { Website = website });
        }

        private void DownloadWebpageData(Client client)
        {
            for (int i = 0; i < Exhibition.Webpages.Count; i++)
            {
                Exhibition.Webpages[i].Data = client.Download(Exhibition.Webpages[i].Url);
            }
        }

        private void AddExhibition(FightPicksContext context)
        {
            Exhibition exhibition = new Exhibition(context, Exhibition.Name, Exhibition.Webpages);
            exhibition.Add();
        }

    }
}
