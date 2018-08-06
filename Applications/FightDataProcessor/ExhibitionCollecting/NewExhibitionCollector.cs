using FightData.Domain;
using FightData.Domain.Entities;

namespace FightDataProcessor
{
    public class NewExhibitionCollector
    {
        private ExhibitionCollectingUi exhibitionUi;
        private Exhibition exhibition;
        private ExhibitionWebpagesCollector exhibitionWebpagesCollector;

        public NewExhibitionCollector() : this(new ExhibitionCollectingUi())
        {

        }

        public NewExhibitionCollector(ExhibitionCollectingUi exhibitionUi)
        {
            this.exhibitionUi = exhibitionUi;
            this.exhibition = new Exhibition(new FightPicksContext());
            this.exhibitionWebpagesCollector = new ExhibitionWebpagesCollector();
        }

        public void CollectNewExhibition()
        {
            InputName();
            exhibition.Webpages = exhibitionWebpagesCollector.CollectExhibitionWebpages();
            exhibition.Add();
        }

        private void InputName()
        {
            exhibition.Name = exhibitionUi.GetExhibitionName();
        }
    }
}
