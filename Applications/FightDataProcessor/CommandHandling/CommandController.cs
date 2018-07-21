using FightData.Domain;
using FightDataProcessor.WebpageParsing;

namespace FightDataProcessor
{
    public class CommandController
    {
        private AppCommand appCommand;
        private UfcEventCollector ufcEventCollector;
        private DataRemover dataRemover;

        public CommandController(AppCommand appCommand)
        {
            this.appCommand = appCommand;
            ufcEventCollector = new UfcEventCollector();
            dataRemover = new DataRemover();
        }

        public void HandleCommand()
        {
            DataUtilities dataUtilities = new DataUtilities();
            
            if (appCommand.Command == Commands.Collect)
            {
                ufcEventCollector.ContinuouslyCollectEvents();
            }
            else if(appCommand.Command == Commands.Process)
            {
                if(appCommand.Arguments.Contains(Arguments.ClearExisting))
                {
                    dataRemover.RemoveAllPicks();
                }

                CollectAllUfcEventsData();
            }
        }

        private void CollectAllUfcEventsData()
        {
            EventsDataExtractor ufcEventsParser = new EventsDataExtractor();
            ufcEventsParser.ParseAllEvents();
        }
    }
}
