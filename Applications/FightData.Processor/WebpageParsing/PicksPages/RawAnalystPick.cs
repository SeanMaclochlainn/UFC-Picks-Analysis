namespace FightData.WebpageParsing.PicksPages
{
    public class RawAnalystPick
    {
        public RawAnalystPick(string analyst, string pick)
        {
            Analyst = analyst;
            Pick = pick;
        }

        public string Analyst { get; private set; }
        public string Pick { get; private set; }
        
    }
}
