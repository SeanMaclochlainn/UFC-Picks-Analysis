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

        public ExhibitionForm(Exhibition existingExhibition)
        {
            Exhibition = existingExhibition;
        }

        public Exhibition Exhibition { get; private set; }

        public void AddWebpages(FightPicksContext context)
        {
            foreach (Website website in new WebsiteFinder(context).GetAllWebsites())
                Exhibition.Webpages.Add(new Webpage(context) { Website = website });
        }

    }
}
