using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class UfcEventGenerator : EntityGenerator
    {
        private WebpageGenerator webpageGenerator;
        private FighterGenerator fighterGenerator;

        public UfcEventGenerator(FightPicksContext context) : base(context)
        {
            webpageGenerator = new WebpageGenerator(context);
            fighterGenerator = new FighterGenerator(context);
        }

        public UfcEvent GetPopulatedUfcEvent()
        {
            UfcEvent ufcEvent = GetEmptyUfcEvent();
            ufcEvent.Webpages = new List<Webpage>() { webpageGenerator.GetPopulatedResultsPage() };
            ufcEvent.AddFight(fighterGenerator.GetWinner(), fighterGenerator.GetLoser());
            return ufcEvent;
        }

        public UfcEvent GetEmptyUfcEvent()
        {
            return new UfcEvent(context) { EventName = "FN55" };
        }
    }
}
