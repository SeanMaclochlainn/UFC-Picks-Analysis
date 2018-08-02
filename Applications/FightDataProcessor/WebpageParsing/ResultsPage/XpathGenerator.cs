namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsTableXpathGenerator
    {
        //private static string winnerXpath = @"//*[@class='toccolours']/tbody/tr[{0}]/td[2]/a|//*[@class='toccolours']/tr[{0}]/td[2]/a";
        //private static string loserXpath = @"//*[@class='toccolours']/tbody/tr[{0}]/td[4]/a|//*[@class='toccolours']/tr[{0}]/td[4]/a";

        private static string winnerXpath = @"//body/table/tr[{0}]/td[1]";
        private static string loserXpath = @"//body/table/tr[{0}]/td[2]";

        public static string GetWinnerXpath(int rowNo)
        {
            return string.Format(winnerXpath, rowNo);
        }

        public static string GetLoserXpath(int rowNo)
        {
            return string.Format(loserXpath, rowNo);
        }
    }
}
