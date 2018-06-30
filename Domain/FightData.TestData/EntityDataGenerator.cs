using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData
{
    public class EntityDataGenerator
    {
        private FightPicksContext context;

        public EntityDataGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public UfcEvent GetUfcEvent()
        {
            UfcEvent ufcEvent = new UfcEvent("test event", context)
            {
                Webpages = new List<Webpage>() { GetWebpage() }
            };
            return ufcEvent;
        }

        public Webpage GetWebpage()
        {
            return new Webpage("url", GetWebsite(), "test data", context);
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
            Fighter fighter = new Fighter("testfname testlname", context);
            //fighter.FirstName = "testfname";
            //fighter.LastName = "testlname";
            return fighter;
        }
    }
}
