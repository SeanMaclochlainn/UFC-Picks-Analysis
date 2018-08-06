using FightData.Domain.Finders;
using FightData.Domain.Entities;
using FightData.Domain;

namespace FightDataProcessor
{
    public class ExistingExhibitionCollector
    {
        private ExhibitionFinder exhibitionFinder;
        private ExhibitionCollectingUi exhibitionUi;
        private Exhibition exhibition;
        private ExhibitionWebpagesCollector exhibitionWebpagesCollector;
        private FightPicksContext context;

        public ExistingExhibitionCollector(FightPicksContext context) : this(new ExhibitionCollectingUi(), new ExhibitionFinder(context), new ExhibitionWebpagesCollector())
        {
            this.context = context;
        }

        public ExistingExhibitionCollector(ExhibitionCollectingUi exhibitionUi, ExhibitionFinder exhibitionFinder, ExhibitionWebpagesCollector exhibitionWebpagesCollector)
        {
            this.exhibitionUi = exhibitionUi;
            this.exhibitionFinder = exhibitionFinder;
            this.exhibitionWebpagesCollector = exhibitionWebpagesCollector;
        }

        public void UpdateExistingExhibition()
        {
            SelectExistingExhibition();
            exhibition.Webpages = exhibitionWebpagesCollector.CollectExhibitionWebpages();
            exhibition.Update();
        }

        private void SelectExistingExhibition()
        {
            exhibition = exhibitionUi.SelectExhibition(exhibitionFinder.FindAllExhibitions());
        }
    }
}
