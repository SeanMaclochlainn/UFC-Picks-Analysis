using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class XpathGenerator
    {
        private static string analystXpath = "//div[@class='articleBody']/table/tbody/tr[{0}]/td/strong";
        private static string fighterXpath = "//div[@class='articleBody']/table/tbody/tr[{0}]/td[{1}]";

        public static string GetAnalystXpath(int rowNo)
        {
            return string.Format(analystXpath, rowNo);
        }

        public static string GetFighterXpath(int rowNo, int columnNo)
        {
            return string.Format(fighterXpath, rowNo, columnNo);
        }
    }
}
