using FightData.Domain.Entities;

namespace FightData.Domain.Builders
{
    public class FightBuilder
    {
        private FighterBuilder fighterBuilder;

        public FightBuilder(FightPicksContext context)
        {
            Context = context;
            fighterBuilder = new FighterBuilder(context);
        }

        public FightPicksContext Context { get; private set; }
        public Exhibition Exhibition { get; private set; }
        public Fighter Winner { get; private set; }
        public Fighter Loser { get; private set; }

        public FightBuilder GenerateFight(Exhibition exhibition, Fighter winner, Fighter loser)
        {
            Exhibition = exhibition;
            Winner = winner;
            Loser = loser;
            return this;
        }

        public FightBuilder GenerateFight(Exhibition exhibition, string winnerName, string loserName)
        {
            Exhibition = exhibition;
            Winner = fighterBuilder.GenerateFighter(winnerName).Build();
            Loser = fighterBuilder.GenerateFighter(loserName).Build();
            return this;
        }

        public Fight Build()
        {
            return new Fight(this);
        }

    }
}
