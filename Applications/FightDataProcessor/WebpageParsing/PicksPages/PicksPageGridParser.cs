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

        public List<ParsedGridRow> ParseRows()
        {
            List<ParsedGridRow> parsedRows = new List<ParsedGridRow>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                ParsedGridRow parsedRowResult = ParseRow(currentRow);
                if(IsValidResult(parsedRowResult))
                    parsedRows.Add(parsedRowResult);
            }
            return parsedRows;
        }

        private ParsedGridRow ParseRow(int rowNo)
        {
            List<string> fighters = fightersParser.ParseFighters(rowNo);
            string analyst = analystParser.ParseAnalyst(rowNo);
            return new ParsedGridRow(analyst, fighters);
        }

        private bool IsValidResult(ParsedGridRow result)
        {
            return !(result.AnalystName == null);
        }
    }
}
