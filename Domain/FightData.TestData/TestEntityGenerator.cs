using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData
{
    public class TestEntityGenerator
    {
        private FightPicksContext context;

        public TestEntityGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public UfcEvent GetPopulatedUfcEvent()
        {
            UfcEvent ufcEvent = new UfcEvent(context)
            {
                EventName = "FN55",
                Webpages = new List<Webpage>() { GetWikipediaPage() }, 
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

        public Webpage GetWikipediaPage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = GetResultsPageWebsite(),
                Data = MockWikipediaPageGenerator.GetHtml()
            };
            return webpage;
        }

        public Website GetResultsPageWebsite()
        {
            return new Website { DomainName = "wikipedia", WebsiteName = WebsiteName.Wikipedia };
        }

        public Webpage GetWebpage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "url",
                Website = GetResultsWebsite(),
                Data = "test data"
            };
            return webpage;
        }

        public Website GetResultsWebsite()
        {
            return new Website() { DomainName = "test name", WebsiteName = WebsiteName.Wikipedia };
        }

        public Analyst GetAnalyst()
        {
            Analyst analyst = new Analyst();
            analyst.AltNames = new List<AnalystAltName>() { GetAnalystAltName(analyst) };
            analyst.Name = "test analyst";
            analyst.Picks = new List<Pick>();
            analyst.Website = GetResultsWebsite();
            return analyst;
        }

        public static AnalystAltName GetAnalystAltName(Analyst analyst)
        {
            AnalystAltName analystAltName = new AnalystAltName();
            analystAltName.Name = "test alt name";
            analystAltName.Analyst = analyst;
            return analystAltName;
        }

        public Pick GetPick()
        {
            Pick pick = new Pick(context);
            pick.Analyst = GetAnalyst();
            pick.Fight = GetFight();
            pick.Fighter = GetFighter();
            return pick;
        }

        public Fight GetFight()
        {
            Fight fight = new Fight(context);
            fight.UfcEvent = GetPopulatedUfcEvent();
            return fight;
        }

        public Fight GetEmptyFight()
        {
            return new Fight(context);
        }

        public Fighter GetFighter()
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames("testfname testlname");
            return fighter;
        }
    }
}
