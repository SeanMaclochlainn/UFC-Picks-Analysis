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
        private Webpage resultsPage;

        public ResultsPageFightExtractor(Webpage resultsPage)
        {
            resultsTableParser = new ResultsTableParser(XDocument.Parse(resultsPage.Data));
            fightAdder = new FightAdder(resultsPage.Event);
            this.resultsPage = resultsPage;
        }

        public void ExtractResults()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();
            foreach(TableRowParserResult parserResult in parserResults)
            {
                if (parserResult.IsRowContainingFight)
                    fightAdder.AddFight(parserResult.Winner, parserResult.Loser);
            }
            resultsPage.Event.Update();
        }

    }
}
