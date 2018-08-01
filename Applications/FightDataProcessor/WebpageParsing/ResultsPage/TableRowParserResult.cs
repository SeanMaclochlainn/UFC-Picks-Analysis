namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class TableRowParserResult
    {
        private TableRowParserResult(string winner, string loser)
        {
            Winner = winner;
            Loser = loser;
        }

        public static TableRowParserResult AsFightRow(string winner, string loser)
        {
            TableRowParserResult tableRowResult = new TableRowParserResult(winner, loser);
            tableRowResult.IsRowContainingFight = true;
            return tableRowResult;
        }

        public static TableRowParserResult AsNonFightRow()
        {
            return new TableRowParserResult("", "");
        }

        public string Winner { get; private set; }
        public string Loser { get; private set; }
        public bool IsRowContainingFight { get; private set; }
    }
}
