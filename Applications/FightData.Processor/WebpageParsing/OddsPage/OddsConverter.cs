using System;

namespace FightData.Processor.WebpageParsing.OddsPage
{
    public class OddsConverter
    {
        public static decimal ConvertToDecimalOdd(string moneylineOdd)
        {
            char firstCharacter = moneylineOdd[0];
            decimal numericalValue = decimal.Parse(moneylineOdd.TrimStart(new char[] { '+', '-' }));
            if (firstCharacter == '+')
                return Math.Round((numericalValue / 100) + 1, 2);
            else
                return Math.Round((100 / numericalValue) + 1, 2);
        }
    }
}
