using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData.EntityGenerators
{
    public class UfcEventGenerator : EntityGenerator
    {
        private WebpageGenerator webpageGenerator;

        public UfcEventGenerator(FightPicksContext context) : base(context)
        {
            webpageGenerator = new WebpageGenerator(context);
        }

        public UfcEvent GetPopulatedUfcEvent()
        {
            UfcEvent ufcEvent = new UfcEvent(context)
            {
                EventName = "FN55",
                Webpages = new List<Webpage>() { webpageGenerator.GetPopulatedResultsPage() },
            };
            ufcEvent = AddFightsToUfcEvent(ufcEvent);
            return ufcEvent;
        }

        public UfcEvent GetEmptyUfcEvent()
        {
            return new UfcEvent(context) { EventName = "test event" };
        }

        private UfcEvent AddFightsToUfcEvent(UfcEvent ufcEvent)
        {
            Fighter winner = Fighter.GenerateFighter("Luke Rockhold", context);
            Fighter loser = Fighter.GenerateFighter("Michael Bisping", context);
            ufcEvent.AddFight(winner, loser);
            return ufcEvent;
        }
    }
}
