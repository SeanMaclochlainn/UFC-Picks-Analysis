using FightData.Domain;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageParser
    {
        private static int maxNoOfRows = 20;
        private PicksPageFightersParser fightersParser;
        private PicksPageAnalystParser analystParser;

        public PicksPageParser(HtmlDocument htmlPage)
        {
            fightersParser = new PicksPageFightersParser(htmlPage);
            analystParser = new PicksPageAnalystParser(htmlPage);
        }

        public List<RawAnalystsPicks> ParsePicksGrid()
        {
            List<RawAnalystsPicks> parsedRows = new List<RawAnalystsPicks>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                RawAnalystsPicks parsedRowResult = ParseRow(currentRow);
                if(IsValidResult(parsedRowResult))
                    parsedRows.Add(parsedRowResult);
            }
            return parsedRows;
        }

        private RawAnalystsPicks ParseRow(int rowNo)
        {
            List<string> fighters = fightersParser.ParseFighters(rowNo);
            string analyst = analystParser.ParseAnalyst(rowNo);
            return new RawAnalystsPicks(analyst, fighters);
        }

        private bool IsValidResult(RawAnalystsPicks result)
        {
            return !(result.AnalystName == null);
        }
    }
}
