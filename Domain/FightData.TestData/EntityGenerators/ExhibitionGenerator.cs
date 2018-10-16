using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class ExhibitionGenerator
    {
        private FightPicksContext context;

        public ExhibitionGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public Exhibition GetParsedExhibition()
        {
            Exhibition exhibition = GetEmptyExhibition();
            exhibition.Webpages = new List<Webpage>() { new WebpageGenerator(context).GetParsedPopulatedResultsPage() };
            exhibition.AddFight(new FighterGenerator(context).GetWinner(), new FighterGenerator(context).GetLoser());
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
