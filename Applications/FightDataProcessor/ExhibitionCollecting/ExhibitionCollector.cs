using FightData.Domain;

namespace FightDataProcessor
{
    //public class ExhibitionCollector
    //{
    //    private ExhibitionCollectingUi exhibitionUi;
    //    private NewExhibitionCollector newExhibitionCollector;
    //    private ExistingExhibitionCollector existingExhibitionCollector;

    //    //public ExhibitionCollector() : this(new ExhibitionCollectingUi(), new FightPicksContext()) { }

    //    public ExhibitionCollector(ExhibitionCollectingUi exhibitionUi, FightPicksContext context)
    //    {
    //        this.exhibitionUi = exhibitionUi;
    //        this.newExhibitionCollector = new NewExhibitionCollector();
    //        this.existingExhibitionCollector = new ExistingExhibitionCollector(context);
    //    }

    //    public void ContinuouslyCollectExhibitions()
    //    {
    //        while (true)
    //            CollectExhibition();
    //    }

    //    public void CollectExhibition()
    //    {
    //        if (exhibitionUi.IsNewExhibition())
    //            newExhibitionCollector.CollectNewExhibition();
    //        else
    //            existingExhibitionCollector.UpdateExistingExhibition();
    //    }
    //}
}