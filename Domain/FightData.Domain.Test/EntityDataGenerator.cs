using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    public static class EntityDataGenerator
    {
        public static UfcEvent UfcEvent()
        {
            UfcEvent ufcEvent = new UfcEvent("test event")
            {
                Webpages = new List<Webpage>() { Webpage() }
            };
            return ufcEvent;
        }

        public static Webpage Webpage()
        {
            return new Webpage("url", Website(), "test data");
        }

        public static Website Website()
        {
            return new Website() { DomainName = "test name", WebsiteName = WebsiteName.Wikipedia };
        }

        public static Analyst Analyst()
        {
            Analyst analyst = new Analyst();
            analyst.AltNames = new List<AnalystAltName>() { AnalystAltName(analyst) };
            analyst.Name = "test analyst";
            analyst.Picks = new List<Pick>();
            analyst.Website = Website();
            return analyst;
        }

        public static AnalystAltName AnalystAltName(Analyst analyst)
        {
            AnalystAltName analystAltName = new AnalystAltName();
            analystAltName.Name = "test alt name";
            analystAltName.Analyst = analyst;
            return analystAltName;
        }

        public static Pick Pick()
        {
            Pick pick = new Pick();
            pick.Analyst = Analyst();
            pick.Fight = Fight();
            pick.FighterPick = Fighter();
            return pick;
        }

        public static Fight Fight()
        {
            Fight fight = new Fight();
            fight.Event = UfcEvent();
            return fight;
        }

        public static Fighter Fighter()
        {
            Fighter fighter = new Fighter();
            fighter.FirstName = "testfname";
            fighter.FullName = "testfname testlname";
            fighter.LastName = "testlname";
            return fighter;
        }
    }
}
