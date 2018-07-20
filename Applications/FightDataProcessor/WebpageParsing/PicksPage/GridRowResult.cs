using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class GridRowResult
    {
        public GridRowResult(string analystName, List<string> fighterNames)
        {
            AnalystName = analystName;
            FighterNames = fighterNames;
        }

        public string AnalystName { get; private set; }
        public List<string> FighterNames { get; private set; }
    }
}
