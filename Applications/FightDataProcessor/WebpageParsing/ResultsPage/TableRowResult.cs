using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class TableRowResult
    {
        private TableRowResult(string winner, string loser)
        {
            Winner = winner;
            Loser = loser;
        }

        public static TableRowResult AsFightRow(string winner, string loser)
        {
            TableRowResult tableRowResult = new TableRowResult(winner, loser);
            tableRowResult.IsRowContainingFight = true;
            return tableRowResult;
        }

        public static TableRowResult AsNonFightRow()
        {
            return new TableRowResult("", "");
        }

        public string Winner { get; private set; }
        public string Loser { get; private set; }
        public bool IsRowContainingFight { get; private set; }
    }
}
