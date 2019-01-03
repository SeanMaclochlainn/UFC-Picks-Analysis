using FightData.Domain.Entities;
using FightData.WebpageParsing.PicksPages;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageParser
    {
        private EntityRowParser entityRowParser;
        private EntityParser entityParser;
        private PicksPageConfiguration picksPageConfiguration;
        private int currentRow;
        private bool validRow;

        public PicksPageParser(HtmlDocument htmlPage)
        {
            entityRowParser = new EntityRowParser(htmlPage);
            entityParser = new EntityParser(htmlPage);
        }

        public List<RawAnalystPick> ParsePicksPage(PicksPageConfiguration picksPageConfiguration)
        {
            this.picksPageConfiguration = picksPageConfiguration;
            List<RawAnalystPick> allRawAnalystPicks = new List<RawAnalystPick>();
            validRow = true;
            currentRow = 1;
            while (validRow)
            {
                List<RawAnalystPick> rawAnalystPicks = ParseCurrentRow();
                if (validRow)
                {
                    foreach (RawAnalystPick analystPick in rawAnalystPicks)
                        allRawAnalystPicks.Add(new RawAnalystPick(analystPick.Analyst, analystPick.Pick));
                }
                currentRow++;
            }
            return allRawAnalystPicks;
        }

        private static bool AreEntitiesValid(string entity, List<string> entityCollection)
        {
            return !string.IsNullOrEmpty(entity) || entityCollection.Count > 0;
        }

        private List<RawAnalystPick> ParseCurrentRow()
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
            validRow = AreEntitiesValid(analyst, fighters);
            foreach (string fighter in fighters)
                rawAnalystPicks.Add(new RawAnalystPick(analyst, fighter));
            return rawAnalystPicks;
        }

        private List<RawAnalystPick> ParseSingleFighterMultipleAnalysts(string analystXpath, string fighterXpath)
        {
            List<RawAnalystPick> rawAnalystPicks = new List<RawAnalystPick>();
            List<string> analysts = entityRowParser.ParseEntities(currentRow, picksPageConfiguration.AnalystXpath, picksPageConfiguration.AnalystRegex);
            string fighter = entityParser.ParseEntity(currentRow, picksPageConfiguration.FighterXpath, picksPageConfiguration.FighterRegex);
            validRow = AreEntitiesValid(fighter, analysts);
            foreach (string analyst in analysts)
                rawAnalystPicks.Add(new RawAnalystPick(analyst, fighter));
            return rawAnalystPicks;
        }
    }
}
