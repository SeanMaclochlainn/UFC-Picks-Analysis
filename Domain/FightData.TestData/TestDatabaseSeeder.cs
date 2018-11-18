using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
            Website oddsWebsite = AddWebsite(WebsiteName.BestFightOdds, WebsiteType.Odds);
            Webpage resultsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = true,
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = resultsWebsite
            };
            Webpage picksWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = true,
                Url = "https://mmajunkie.com/2014/11/ufc-fight-night-55-staff-picks-rockhold-a-unanimous-nod-over-bisping",
                Website = picksWebsite
            };
            Webpage oddsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = true,
                Url = "https://www.bestfightodds.com/events/ufc-179-aldo-vs-mendes-ii-855#",
                Website = oddsWebsite,
                Data = GetResourceFile("OddsPage.html")
            };
            context.Webpages.AddRange(new List<Webpage>() { resultsWebpage, picksWebpage, oddsWebpage });
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

        private static string GetResourceFile(string fileName)
        {
            string fileData = "";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string currentAssemblyName = currentAssembly.GetName().Name;
            string folderName = "WebsiteHtml";
            using (Stream stream = currentAssembly.GetManifestResourceStream($"{currentAssemblyName}.{folderName}.{fileName}"))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    fileData = sr.ReadToEnd();
                }
            }
            return fileData;
        }
    }
}
