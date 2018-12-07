using FightData.Domain.Entities;
using FightData.WebpageParsing.PicksPages;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageParser
    {
        private static int maxNoOfRows = 20;
        private EntityRowParser entityRowParser;
        private EntityParser entityParser;
        private PicksPageConfiguration picksPageConfiguration;
        private int currentRow;

        public PicksPageParser(HtmlDocument htmlPage)
        {
            entityRowParser = new EntityRowParser(htmlPage);
            entityParser = new EntityParser(htmlPage);
        }

        public List<RawAnalystPick> ParsePicksPage(PicksPageConfiguration picksPageConfiguration)
        {
            this.picksPageConfiguration = picksPageConfiguration;
            List<RawAnalystPick> allRawAnalystPicks = new List<RawAnalystPick>();
            for (int currentRow = 1; currentRow <= maxNoOfRows; currentRow++)
            {
                this.currentRow = currentRow;
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
            if (picksPageConfiguration.PicksPageRowType == PicksPageRowType.SingleAnalystMultipleFighters)
            {
                return ParseSingleAnalystMultipleFighters(picksPageConfiguration.AnalystXpath, picksPageConfiguration.FighterXpath);
            }
            else
            {
                return ParseSingleFighterMultipleAnalysts(picksPageConfiguration.AnalystXpath, picksPageConfiguration.FighterXpath);
            }
        }

        private List<RawAnalystPick> ParseSingleAnalystMultipleFighters(string analystXpath, string fighterXpath)
        {
            List<RawAnalystPick> rawAnalystPicks = new List<RawAnalystPick>();
            List<string> fighters = entityRowParser.ParseEntities(currentRow, picksPageConfiguration.FighterXpath, picksPageConfiguration.FighterRegex);
            string analyst = entityParser.ParseEntity(currentRow, picksPageConfiguration.AnalystXpath, picksPageConfiguration.AnalystRegex);
            foreach (string fighter in fighters)
                rawAnalystPicks.Add(new RawAnalystPick(analyst, fighter));
            return rawAnalystPicks;
        }

        private List<RawAnalystPick> ParseSingleFighterMultipleAnalysts(string analystXpath, string fighterXpath)
        {
            List<RawAnalystPick> rawAnalystPicks = new List<RawAnalystPick>();
            List<string> analysts = entityRowParser.ParseEntities(currentRow, picksPageConfiguration.AnalystXpath, picksPageConfiguration.AnalystRegex);
            string fighter = entityParser.ParseEntity(currentRow, picksPageConfiguration.FighterXpath, picksPageConfiguration.FighterRegex);
            foreach (string analyst in analysts)
                rawAnalystPicks.Add(new RawAnalystPick(analyst, fighter));
            return rawAnalystPicks;
        }

        private bool IsValidRow(List<RawAnalystPick> rawAnalystPicks)
        {
            return rawAnalystPicks.Count != 0;
        }
    }
}
