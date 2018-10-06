using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightDataUI.ViewModels
{
    public class CreateExhibitionView
    {
        public CreateExhibitionView()
        {
            Exhibition = new Exhibition();
        }

        public Exhibition Exhibition { get; private set; }

        public void LoadViewData(FightPicksContext context)
        {
            AddAllWebsiteWebpages(context);
        }

        private void AddAllWebsiteWebpages(FightPicksContext context)
        {
            foreach (Website website in new WebsiteFinder(context).FindAllWebsites())
                Exhibition.Webpages.Add(new Webpage(context) { Website = website });
        }
    }
}
