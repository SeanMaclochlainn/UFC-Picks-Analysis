using System.Collections.Generic;

namespace FightData.Domain
{
    public class RawAnalystsPicks
    {
        public RawAnalystsPicks(string analystName, List<string> fighterNames)
        {
            AnalystName = analystName;
            FighterNames = fighterNames;
        }

        public string AnalystName { get; private set; }
        public List<string> FighterNames { get; private set; }
    }
}
