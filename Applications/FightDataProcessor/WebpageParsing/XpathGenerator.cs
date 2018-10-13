namespace FightDataProcessor.WebpageParsing
{
    public class XpathGenerator
    {
        public static string ResultsPageWinnerXpath(int rowNo)
        {
            return string.Format(@"//body//table[2]//tr[{0}]/td[2]", rowNo);
        }

        public static string ResultsPageLoserXpath(int rowNo)
        {
            return string.Format(@"//body//table[2]//tr[{0}]/td[4]", rowNo);
        }

        public static string PicksPageAnalystXpath(int rowNo)
        {
            return string.Format("//table/tr[{0}]/td[1]", rowNo);
        }

        public static string PicksPageFighterXpath(int rowNo, int columnNo)
        {
            return string.Format("//table/tr[{0}]/td[{1}+1]", rowNo, columnNo);
        }
    }
}
