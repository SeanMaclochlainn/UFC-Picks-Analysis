using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain.Updaters
{
    public class ExhibitionUpdater
    {
        private FightPicksContext context;
        private ExhibitionFinder exhibitionFinder;

        public ExhibitionUpdater(FightPicksContext context)
        {
            this.context = context;
            exhibitionFinder = new ExhibitionFinder(context);
        }

        public void AddExhibition(ExhibitionForm exhibitionForm, Client client)
        {
            List<Webpage> webpages = DownloadWebpageData(exhibitionForm.Exhibition.Webpages, client);
            Exhibition exhibition = new Exhibition(context, exhibitionForm.Exhibition.Name, webpages);
            exhibition.Add();
        }

        public void UpdateExhibition(ExhibitionForm exhibitionForm, Client client)
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition(exhibitionForm.Exhibition.Id);
            exhibition.Name = exhibitionForm.Exhibition.Name;
            exhibition.Webpages = DownloadWebpageData(exhibitionForm.Exhibition.Webpages, client);
            exhibition.Update();
        }

        private List<Webpage> DownloadWebpageData(List<Webpage> webpages, Client client)
        {
            List<Webpage> populatedWebpages = new List<Webpage>();
            foreach (Webpage webpageInput in webpages)
            {
                Webpage webpage = new Webpage(context, webpageInput.Url, webpageInput.Website);
                webpage.DownloadData(client);
                populatedWebpages.Add(webpage);
            }
            return populatedWebpages;
        }
    }
}
