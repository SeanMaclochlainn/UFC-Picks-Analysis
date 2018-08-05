using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class ParsedGridRow
    {
        public ParsedGridRow(string analystName, List<string> fighterNames)
        {
            AnalystName = analystName;
            FighterNames = fighterNames;
        }

        public string AnalystName { get; private set; }
        public List<string> FighterNames { get; private set; }
    }
}
