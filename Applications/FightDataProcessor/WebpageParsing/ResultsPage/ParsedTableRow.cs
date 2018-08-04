namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ParsedTableRow
    {
        public ParsedTableRow(string winner, string loser)
        {
            Winner = winner;
            Loser = loser;
        }

        public string Winner { get; private set; }
        public string Loser { get; private set; }
    }
}
