using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.FightData.Domain;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageFightExtractor
    {
        private ResultsTableParser resultsTableParser;
        private FightAdder fightAdder;

        public ResultsPageFightExtractor(UfcEvent ufcEvent, FightPicksContext context)
        {
            HtmlDocument resultsPage = HtmlDocumentGenerator.FromWebpage(ufcEvent.GetResultsPage());
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
