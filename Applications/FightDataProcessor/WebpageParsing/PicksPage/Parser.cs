using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class PicksPageParser
    {
        private HtmlDocument htmlDocument;
        private EventUpdater eventDataUpdater;
        private int maxNoOfGridRows = 20;

        public PicksPageParser(HtmlDocument htmlDocument, UfcEvent ufcEvent)
        {
            this.htmlDocument = htmlDocument;
            eventDataUpdater = new EventUpdater(ufcEvent);
        }

        public void ParsePickGrid()
        {
            for (int rowNo = 0; rowNo <= maxNoOfGridRows; rowNo++)
            {
                GridRowParser gridRowParser = new GridRowParser(htmlDocument, rowNo);
                if(gridRowParser.IsValidRow())
                {
                    //PicksGenerator picksGenerator = new PicksGenerator(gridRowParser.GridRowResult);
                }
                
                //Analyst analyst;
                //AnalystFinder analystFinder = new AnalystFinder(gridRowParser.AnalystName);
                //if (analystFinder.AnalystExists)
                //    analyst = analystFinder.Analyst;

            }
        }

        
    }
}
