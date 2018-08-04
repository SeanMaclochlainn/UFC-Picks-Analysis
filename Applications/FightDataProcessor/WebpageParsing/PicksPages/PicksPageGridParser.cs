using System.Collections.Generic;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageGridParser
    {
        private static int maxNoOfRows = 20;
        private FightersParser fightersParser;
        private AnalystParser analystParser;

        public PicksPageGridParser(XDocument htmlPage)
        {
            fightersParser = new FightersParser(htmlPage);
            analystParser = new AnalystParser(htmlPage);
        }

        public List<GridRowResult> ParseRows()
        {
            List<GridRowResult> parsedRows = new List<GridRowResult>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                GridRowResult parsedRowResult = ParseRow(currentRow);
                if(IsValidResult(parsedRowResult))
                    parsedRows.Add(parsedRowResult);
            }
            return parsedRows;
        }

        private GridRowResult ParseRow(int rowNo)
        {
            List<string> fighters = fightersParser.ParseFighters(rowNo);
            string analyst = analystParser.ParseAnalyst(rowNo);
            return new GridRowResult(analyst, fighters);
        }

        private bool IsValidResult(GridRowResult result)
        {
            return !(result.AnalystName == null);
        }
    }
}
