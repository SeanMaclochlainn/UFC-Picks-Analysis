using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class PicksGridParser
    {
        private HtmlDocument htmlDocument;
        private int maxNoOfGridRows = 20;
        private UfcEvent ufcEvent;

        public PicksGridParser(HtmlDocument htmlDocument, UfcEvent ufcEvent)
        {
            this.htmlDocument = htmlDocument;
            this.ufcEvent = ufcEvent;
        }

        public void Parse()
        {
            for (int rowNo = 0; rowNo <= maxNoOfGridRows; rowNo++)
            {
                GridRowParser gridRowParser = new GridRowParser(htmlDocument, rowNo);
                if(gridRowParser.IsValidRow())
                {
                    PicksCollector picksCollector = new PicksCollector(gridRowParser.GridRowResult, ufcEvent);
                }
                
                //Analyst analyst;
                //AnalystFinder analystFinder = new AnalystFinder(gridRowParser.AnalystName);
                //if (analystFinder.AnalystExists)
                //    analyst = analystFinder.Analyst;

            }
        }

        
    }
}
