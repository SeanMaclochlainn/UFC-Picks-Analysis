using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.TestData
{
    public class TestDatabaseSeeder
    {
        private FightPicksContext context;

        public TestDatabaseSeeder(FightPicksContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            AddFN55();
        }

        private void AddFN55()
        {
            Exhibition exhibition = AddExhibition("FN 55");
            Fighter winner = AddFighter("Luke Rockhold");
            Fighter loser = AddFighter("Michael Bisping");
            Fight fight = new Fight(context)
            {
                Exhibition = exhibition,
                Winner = winner,
                Loser = loser
            };
            context.Fights.Add(fight);
            context.SaveChanges();

            Pick correctPick = new Pick(context)
            {
                Analyst = AddAnalyst("Mike Bohn"),
                Fight = fight,
                Fighter = winner
            };
            Pick incorrectPick = new Pick(context)
            {
                Analyst = AddAnalyst("Dann Stupp"),
                Fight = fight,
                Fighter = loser
            };
            context.Picks.AddRange(new List<Pick>() { correctPick, incorrectPick });
            context.SaveChanges();

            Website resultsWebsite = AddWebsite(WebsiteName.Wikipedia, WebsiteType.Result);
            Website picksWebsite = AddWebsite(WebsiteName.MMAJunkie, WebsiteType.Pick);
            Webpage resultsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = false,
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = resultsWebsite
            };
            Webpage picksWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = false,
                Url = "https://mmajunkie.com/2014/11/ufc-fight-night-55-staff-picks-rockhold-a-unanimous-nod-over-bisping",
                Website = picksWebsite
            };
            context.Webpages.AddRange(new List<Webpage>() { resultsWebpage, picksWebpage });
            context.SaveChanges();
        }

        private Fighter AddFighter(string name)
        {
            Fighter fighter = Fighter.GenerateFighter(name, context);
            context.Fighters.Add(fighter);
            context.SaveChanges();
            return fighter;
        }

        private Exhibition AddExhibition(string name)
        {
            Exhibition exhibition = new Exhibition(context, name, new List<Webpage>());
            context.Exhibitions.Add(exhibition);
            context.SaveChanges();
            return exhibition;
        }

        private Analyst AddAnalyst(string name)
        {
            Analyst analyst = new Analyst(context)
            {
                Name = name
            };
            context.Analysts.Add(analyst);
            context.SaveChanges();
            return analyst;
        }

        private Website AddWebsite(WebsiteName websiteName, WebsiteType websiteType)
        {
            Website website = new Website(context)
            {
                WebsiteName = websiteName,
                WebsiteType = websiteType
            };
            context.Websites.Add(website);
            context.SaveChanges();
            return website;
        }
    }
}
