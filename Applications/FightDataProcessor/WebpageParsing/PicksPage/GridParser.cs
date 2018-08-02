using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class GridParser
    {
        private static int maxNoOfRows = 20;
        private int currentRow;
        private HtmlDocument htmlDocument;
        private FightersParser fightersParser;
        private AnalystParser analystParser;

        public GridParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
            fightersParser = new FightersParser(htmlDocument);
            analystParser = new AnalystParser(htmlDocument);
        }

        public List<GridRowResult> ParseRows()
        {
            List<GridRowResult> parsedRows = new List<GridRowResult>();
            for(int i=0;i<maxNoOfRows;i++)
            {
                currentRow = i;
                parsedRows.Add(ParseCurrentRow());
            }
            return parsedRows;
        }

        private GridRowResult ParseCurrentRow()
        {
            List<string> fighters = fightersParser.ParseFighters(currentRow);
            string analyst = analystParser.ParseAnalyst(currentRow);
            return new GridRowResult(analyst, fighters);
        }
    }
}
