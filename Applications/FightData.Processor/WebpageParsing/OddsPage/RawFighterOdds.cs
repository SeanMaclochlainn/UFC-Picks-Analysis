namespace FightData.Processor.WebpageParsing.OddsPage
{
    public class RawFighterOdds
    {
        public RawFighterOdds(string fighterName, string odds)
        {
            FighterName = fighterName;
            Odds = odds;
        }

        public string FighterName { get; private set; }
        public string Odds { get; private set; }
    }
}
