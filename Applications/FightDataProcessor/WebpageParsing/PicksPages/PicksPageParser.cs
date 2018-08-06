using FightData.Domain;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageParser
    {
        private static int maxNoOfRows = 20;
        private FightersParser fightersParser;
        private AnalystParser analystParser;

        public PicksPageParser(XDocument htmlPage)
        {
            fightersParser = new FightersParser(htmlPage);
            analystParser = new AnalystParser(htmlPage);
        }

        public List<RawUfcEventPicks> ParsePicksGrid()
        {
            List<RawUfcEventPicks> parsedRows = new List<RawUfcEventPicks>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                RawUfcEventPicks parsedRowResult = ParseRow(currentRow);
                if(IsValidResult(parsedRowResult))
                    parsedRows.Add(parsedRowResult);
            }
            return parsedRows;
        }

        private RawUfcEventPicks ParseRow(int rowNo)
        {
            List<string> fighters = fightersParser.ParseFighters(rowNo);
            string analyst = analystParser.ParseAnalyst(rowNo);
            return new RawUfcEventPicks(analyst, fighters);
        }

        private bool IsValidResult(RawUfcEventPicks result)
        {
            return !(result.AnalystName == null);
        }
    }
}
