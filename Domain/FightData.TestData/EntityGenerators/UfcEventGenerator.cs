using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class UfcEventGenerator
    {
        private FightPicksContext context;

        public UfcEventGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public UfcEvent GetPopulatedUfcEvent()
        {
            UfcEvent ufcEvent = GetEmptyUfcEvent();
            ufcEvent.Webpages = new List<Webpage>() { new WebpageGenerator(context).GetPopulatedResultsPage() };
            ufcEvent.AddFight(new FighterGenerator(context).GetWinner(), new FighterGenerator(context).GetLoser());
            return ufcEvent;
        }

        public UfcEvent GetEmptyUfcEvent()
        {
            return new UfcEvent(context) { EventName = "FN55" };
        }
    }
}
