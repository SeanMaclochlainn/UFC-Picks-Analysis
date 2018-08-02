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

        public ResultsPageFightExtractor(UfcEvent ufcEvent, FightPicksContext context)
        {
            XDocument resultsPage = XDocument.Parse(ufcEvent.GetResultsPage().Data);
            resultsTableParser = new ResultsTableParser(resultsPage);
            fightAdder = new FightAdder(ufcEvent, context);
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
