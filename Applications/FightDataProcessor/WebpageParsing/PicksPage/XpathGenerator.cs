namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class XpathGenerator
    {
        public static string GetAnalystXpath(int rowNo)
        {
            //return string.Format("//div[@class='articleBody']/table/tbody/tr[{0}]/td/strong", rowNo);
            return string.Format("//table/tr[{0}]/td[1]", rowNo);
        }

        public static string GetFighterXpath(int rowNo, int columnNo)
        {
            //return string.Format("//div[@class='articleBody']/table/tbody/tr[{0}]/td[{1}+1]", rowNo, columnNo);
            return string.Format("//table/tr[{0}]/td[{1}+1]", rowNo, columnNo);
        }
    }
}
