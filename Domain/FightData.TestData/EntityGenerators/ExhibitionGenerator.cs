using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Updaters;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class ExhibitionGenerator
    {
        private FightPicksContext context;
        private FighterGenerator fighterGenerator;
        private AnalystGenerator analystGenerator;


        public ExhibitionGenerator(FightPicksContext context)
        {
            this.context = context;
            analystGenerator = new AnalystGenerator(context);
            fighterGenerator = new FighterGenerator(context);
        }

        public Exhibition GetParsedExhibition()
        {
            Exhibition exhibition = GetEmptyExhibition();
            exhibition.Webpages = new List<Webpage>() { new WebpageGenerator(context).GetParsedPopulatedResultsPage() };
            Fighter winner = fighterGenerator.GetWinner();
            Fighter loser = fighterGenerator.GetLoser();
            Fight fight = new Fight(context)
            {
                Winner = winner,
                Loser = loser,
                Exhibition = exhibition
            };
            Pick correctPick = new Pick(context)
            {
                Analyst = analystGenerator.GetPopulatedAnalyst(),
                Fight = fight,
                Fighter = winner
            };
            Pick incorrectPick = new Pick(context)
            {
                Analyst = analystGenerator.GetPopulatedAnalyst(),
                Fight = fight,
                Fighter = loser
            };
            fight.Picks.Add(correctPick);
            fight.Picks.Add(incorrectPick);
            exhibition.Fights.Add(fight);
            return exhibition;
        }

        public Exhibition GetEmptyExhibition()
        {
            return new Exhibition(context) { Name = "FN55" };
        }

        public Exhibition GetUnparsedExhibition()
        {
            Exhibition exhibition = GetEmptyExhibition();
            exhibition.Webpages = new List<Webpage>() { new WebpageGenerator(context).GetPopulatedResultsPage(), new WebpageGenerator(context).GetPopulatedPicksPage() };
            return exhibition;
        }
    }
}
