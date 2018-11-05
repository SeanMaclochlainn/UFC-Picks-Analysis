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

        public List<RawAnalystPick> ParsePicksGrid()
        {
            List<RawAnalystPick> allRawAnalystPicks = new List<RawAnalystPick>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                List<RawAnalystPick> rawAnalystPicks = ParseRow(currentRow);
                if(IsValidRow(rawAnalystPicks))
                {
                    foreach (RawAnalystPick analystPick in rawAnalystPicks)
                        allRawAnalystPicks.Add(new RawAnalystPick(analystPick.Analyst, analystPick.Pick));
                }
            }
            return allRawAnalystPicks;
        }

        private List<RawAnalystPick> ParseRow(int rowNo)
        {
            List<string> fighters = fightersParser.ParseFighters(rowNo);
            string analyst = analystParser.ParseAnalyst(rowNo);
            List<RawAnalystPick> rawAnalystPicks = new List<RawAnalystPick>();
            foreach (string fighter in fighters)
                rawAnalystPicks.Add(new RawAnalystPick(analyst, fighter));
            return rawAnalystPicks;
        }

        private bool IsValidRow(List<RawAnalystPick> rawAnalystPicks)
        {
            return rawAnalystPicks.Count != 0;
        }
    }
}
