using FightData.Domain.Entities;
using FightData.Domain.Finders;

namespace FightData.Domain.EntityCreation
{
    public class ExhibitionForm
    {
        public ExhibitionForm()
        {
            Exhibition = new Exhibition();
        }

        public ExhibitionForm(Exhibition newExhibition)
        {
            Exhibition = newExhibition;
        }

        public Exhibition Exhibition { get; private set; }

        public void LoadDataForInput(FightPicksContext context, Exhibition exhibition)
        {
            exhibition = Exhibition;
            AddAllWebsiteWebpages(context);
        }

        private void AddAllWebsiteWebpages(FightPicksContext context)
        {
            foreach (Website website in new WebsiteFinder(context).FindAllWebsites())
                Exhibition.Webpages.Add(new Webpage(context) { Website = website });
        }

    }
}
