using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.FightData.Domain;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageFightExtractor
    {
        private ResultsTableParser resultsTableParser;
        private FightAdder fightAdder;

        public ResultsPageFightExtractor(UfcEvent ufcEvent)
        {
            resultsTableParser = new ResultsTableParser(XDocument.Parse(ufcEvent.GetResultsPage().Data));
            fightAdder = new FightAdder(ufcEvent);
        }

        public void ExtractFights()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();
            foreach(TableRowParserResult parserResult in parserResults)
            {
                if (parserResult.IsRowContainingFight)
                    fightAdder.AddFight(parserResult.Winner, parserResult.Loser);
            }
        }

    }
}
