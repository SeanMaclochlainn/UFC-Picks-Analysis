namespace FightData.Domain
{
    public class FightResult
    {
        public FightResult(string winner, string loser)
        {
            Winner = winner;
            Loser = loser;
        }

        public string Winner { get; private set; }
        public string Loser { get; private set; }
    }
}
