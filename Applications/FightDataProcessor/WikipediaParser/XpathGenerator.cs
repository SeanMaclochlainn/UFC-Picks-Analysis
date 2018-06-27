using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WikipediaParser
{
    public class XpathGenerator
    {
        private static string winnerXpath = @"//*[@class='toccolours']/tbody/tr[{0}]/td[2]/a|//*[@class='toccolours']/tr[{0}]/td[2]/a";
        private static string loserXpath = @"//*[@class='toccolours']/tbody/tr[{0}]/td[4]/a|//*[@class='toccolours']/tr[{0}]/td[4]/a";

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
