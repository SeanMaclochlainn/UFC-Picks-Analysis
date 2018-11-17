using FightData.Domain.Entities;
using FightData.Domain.EntityCreation;
using FightData.Domain.Finders;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Updaters
{
    public class ExhibitionUpdater
    {
        private FightPicksContext context;
        private ExhibitionFinder exhibitionFinder;
        private FightUpdater fightUpdater;

        public ExhibitionUpdater(FightPicksContext context)
        {
            this.context = context;
            exhibitionFinder = new ExhibitionFinder(context);
            fightUpdater = new FightUpdater(context);
        }

        public void Add(ExhibitionForm exhibitionForm)
        {
            Add(exhibitionForm, new ConnectedClient());
        }

        public void Add(ExhibitionForm exhibitionForm, Client client)
        {
            List<Webpage> webpages = DownloadWebpageData(exhibitionForm.Exhibition.Webpages, client);
            Exhibition exhibition = new Exhibition(context, exhibitionForm.Exhibition.Name, webpages);
            Add(exhibition);
        }

        public void Add(Exhibition exhibition)
        {
            context.Exhibitions.Add(exhibition);
            AddWebsitesToContext(exhibition);
            context.SaveChanges();
        }

        private void AddWebsitesToContext(Exhibition exhibition)
        {
            context.Websites.AttachRange(exhibition.Webpages.Select(w => w.Website));
        }

        public void UpdateExhibition(ExhibitionForm exhibitionForm, Client client)
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition(exhibitionForm.Exhibition.Id);
            exhibition.Name = exhibitionForm.Exhibition.Name;
            exhibition.Webpages = DownloadWebpageData(exhibitionForm.Exhibition.Webpages, client);
            context.SaveChanges();
        }

        public void Delete(Exhibition exhibition)
        {
            context.Exhibitions.Remove(exhibition);
            context.SaveChanges();
        }

        public void DeleteParsedData(Exhibition exhibition)
        {
            fightUpdater.DeleteFights(exhibition.Fights);
            new WebpageUpdater(context).MarkAsUnparsed(exhibition.Webpages);
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
