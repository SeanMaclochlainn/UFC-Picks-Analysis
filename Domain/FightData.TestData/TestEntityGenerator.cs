using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData
{
    public class TestEntityGenerator
    {
        private FightPicksContext context;

        public TestEntityGenerator()
        {
            context = new TestDatabase().Context;
        }

        public TestEntityGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public UfcEvent GetUfcEvent()
        {
            UfcEvent ufcEvent = new UfcEvent(context)
            {
                EventName = "test event",
                Webpages = new List<Webpage>() { GetWebpage() }
            };
            return ufcEvent;
        }

        public UfcEvent GetStandardUfcEvent()
        {
            UfcEvent ufcEvent = new UfcEvent(context)
            {
                EventName = "FN55",
                Webpages = new List<Webpage>() { GetStandardWikipidiaPage() }
            };
            return ufcEvent;
        }

        public Webpage GetStandardWikipidiaPage()
        {
            Webpage webpage = new Webpage(context)
            {
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = GetResultsPageWebsite(),
                Data = MockWikipediaPageGenerator.GetStandardPageHtml()
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
                Website = GetWebsite(),
                Data = "test data"
            };
            return webpage;
        }

        public Website GetWebsite()
        {
            return new Website() { DomainName = "test name", WebsiteName = WebsiteName.Wikipedia };
        }

        public Analyst GetAnalyst()
        {
            Analyst analyst = new Analyst();
            analyst.AltNames = new List<AnalystAltName>() { GetAnalystAltName(analyst) };
            analyst.Name = "test analyst";
            analyst.Picks = new List<Pick>();
            analyst.Website = GetWebsite();
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
            Pick pick = new Pick();
            pick.Analyst = GetAnalyst();
            pick.Fight = GetFight();
            pick.FighterPick = GetFighter();
            return pick;
        }

        public Fight GetFight()
        {
            Fight fight = new Fight(context);
            fight.UfcEvent = GetUfcEvent();
            return fight;
        }

        public Fighter GetFighter()
        {
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames("testfname testlname");
            //fighter.FirstName = "testfname";
            //fighter.LastName = "testlname";
            return fighter;
        }
    }
}
