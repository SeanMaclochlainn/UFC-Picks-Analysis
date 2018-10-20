using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightDataProcessor.WebpageParsing;

namespace FightDataProcessor
{
    //public class CommandController
    //{
    //    private AppCommand appCommand;
    //    private ExhibitionCollector exhibitionCollector;
    //    private DataRemover dataRemover;

    //    public CommandController(AppCommand appCommand)
    //    {
    //        this.appCommand = appCommand;
    //        exhibitionCollector = new ExhibitionCollector();
    //        dataRemover = new DataRemover(new FightPicksContext());
    //    }

    //    public void HandleCommand()
    //    {
    //        DataUtilities dataUtilities = new DataUtilities();
            
    //        if (appCommand.Command == Commands.Collect)
    //        {
    //            exhibitionCollector.ContinuouslyCollectExhibitions();
    //        }
    //        else if(appCommand.Command == Commands.Process)
    //        {
    //            if(appCommand.Arguments.Contains(Arguments.ClearExisting))
    //            {
    //                dataRemover.RemoveAllPicks();
    //            }

    //            CollectAllExhibitionData();
    //        }
    //    }

    //    private void CollectAllExhibitionData()
    //    {
    //        ExhibitionFinder exhibitionFinder = new ExhibitionFinder(new FightPicksContext());
    //        foreach (Exhibition exhibition in exhibitionFinder.FindAllExhibitions())
    //            new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();
    //    }
    //}
}
