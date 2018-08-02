using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class PicksPageDataExtractor
    {
        private HtmlDocument htmlDocument;
        private PickAdder pickAdder;
        private AnalystFinder analystFinder;

        public PicksPageDataExtractor(HtmlDocument htmlDocument, UfcEvent ufcEvent, FightPicksContext context)
        {
            this.htmlDocument = htmlDocument;
            pickAdder = new PickAdder(ufcEvent, context);
            analystFinder = new AnalystFinder(context);
        }

        public void ExtractGridData()
        {
            GridParser gridParser = new GridParser(htmlDocument);
            List<GridRowResult> gridRowResults = gridParser.ParseRows();
            foreach (GridRowResult gridRowResult in gridRowResults)
            {
                if (gridRowResult.IsValidRow())
                    ExtractRowData(gridRowResult);
            }
        }

        private void ExtractRowData(GridRowResult gridRowResult)
        {
            Analyst analyst = analystFinder.FindAnalyst(gridRowResult.AnalystName).Result;
            foreach (string fighterName in gridRowResult.FighterNames)
                pickAdder.AddPick(analyst, fighterName);
        }
    }
}
