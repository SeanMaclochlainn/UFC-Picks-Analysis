namespace FightDataProcessor.WebpageParsing
{
    public class XpathGenerator
    {
        public static string ResultsPageWinnerXpath(int row)
        {
            return string.Format(@"//body//table[@class='toccolours']//tr[{0}]/td[2]", row);
        }

        public static string ResultsPageLoserXpath(int row)
        {
            return string.Format(@"//body//table[@class='toccolours']//tr[{0}]/td[4]", row);
        }

        public static string ResultsPageDate()
        {
            return @"//span[@class='bday dtstart published updated']";
        }

        public static string OddsPageFighter(int row)
        {
            return string.Format("//table[@class='odds-table']/tbody/tr[@class='even' or @class='odd'][{0}]//span", row);
        }

        public static string OddsPageOdds(int row)
        {
            return string.Format("//table[@class='odds-table']/tbody/tr[@class='even' or @class='odd'][{0}]/td[2]//span/span", row);
        }

        public static string FormatXpath(string xpath, int row)
        {
            return xpath.Replace("{row-incrementer}", row.ToString());
        }

        public static string FormatXpath(string xpath, int row, int column)
        {
            xpath = xpath.Replace("{column-incrementer}", column.ToString());
            return xpath.Replace("{row-incrementer}", row.ToString());
        }
    }
}
