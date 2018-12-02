namespace FightDataProcessor.WebpageParsing
{
    public class XpathGenerator
    {
        public static string ResultsPageWinnerXpath(int rowNo)
        {
            return string.Format(@"//body//table[@class='toccolours']//tr[{0}]/td[2]", rowNo);
        }

        public static string ResultsPageLoserXpath(int rowNo)
        {
            return string.Format(@"//body//table[@class='toccolours']//tr[{0}]/td[4]", rowNo);
        }

        public static string OddsPageFighter(int rowNo)
        {
            return string.Format("//table[@class='odds-table']/tbody/tr[@class='even' or @class='odd'][{0}]//span", rowNo);
        }

        public static string OddsPageOdds(int rowNo)
        {
            return string.Format("//table[@class='odds-table']/tbody/tr[@class='even' or @class='odd'][{0}]/td[2]//span/span", rowNo);
        }

        public static string FormatXpath(string xpath, int rowNo)
        {
            return string.Format(xpath, rowNo);
        }

        public static string FormatXpath(string xpath, int rowNo, int colNo)
        {
            return string.Format(xpath, rowNo, colNo);
        }
    }
}
