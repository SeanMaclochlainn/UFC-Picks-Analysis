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

        public List<RawExhibitionPicks> ParsePicksGrid()
        {
            List<RawExhibitionPicks> parsedRows = new List<RawExhibitionPicks>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                RawExhibitionPicks parsedRowResult = ParseRow(currentRow);
                if(IsValidResult(parsedRowResult))
                    parsedRows.Add(parsedRowResult);
            }
            return parsedRows;
        }

        private RawExhibitionPicks ParseRow(int rowNo)
        {
            List<string> fighters = fightersParser.ParseFighters(rowNo);
            string analyst = analystParser.ParseAnalyst(rowNo);
            return new RawExhibitionPicks(analyst, fighters);
        }

        private bool IsValidResult(RawExhibitionPicks result)
        {
            return !(result.AnalystName == null);
        }
    }
}
