using System.Collections.Generic;

namespace FightData.Domain
{
    public class RawExhibitionPicks
    {
        public RawExhibitionPicks(string analystName, List<string> fighterNames)
        {
            AnalystName = analystName;
            FighterNames = fighterNames;
        }

        public string AnalystName { get; private set; }
        public List<string> FighterNames { get; private set; }
    }
}
