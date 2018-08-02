using System.Collections.Generic;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class GridParser
    {
        private static int maxNoOfRows = 20;
        private int currentRow;
        private XDocument htmlPage;
        private FightersParser fightersParser;
        private AnalystParser analystParser;

        public GridParser(XDocument htmlPage)
        {
            this.htmlPage = htmlPage;
            fightersParser = new FightersParser(htmlPage);
            analystParser = new AnalystParser(htmlPage);
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
