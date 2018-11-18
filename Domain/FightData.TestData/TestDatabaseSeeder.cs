using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FightData.TestData
{
    public class TestDatabaseSeeder
    {
        private FightPicksContext context;
        private EntityFinder entityFinder;

        public TestDatabaseSeeder(FightPicksContext context)
        {
            this.context = context;
            entityFinder = new EntityFinder(context);
        }

        public void Seed()
        {
            AddWebsite(WebsiteName.Wikipedia, WebsiteType.Result);
            AddWebsite(WebsiteName.MMAJunkie, WebsiteType.Pick);
            AddWebsite(WebsiteName.BestFightOdds, WebsiteType.Odds);
            AddAnalyst("Mike Bohn");
            AddAnalyst("Dann Stupp");
            AddFN55();
            AddUFC179();
        }

        private void AddUFC179()
        {
            Exhibition exhibition = AddExhibition("UFC 179");

            Webpage resultsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = false,
                Url = "https://en.wikipedia.org/wiki/UFC_179",
                Website = entityFinder.WebsiteFinder.GetWebsite(WebsiteName.Wikipedia),
                Data = GetResourceFile("UFC179.html", "ResultsPages")
            };

            Webpage picksWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = false,
                Url = "https://mmajunkie.com/2014/10/ufc-179-staff-picks-splits-with-aldo-vs-mendes-teixeira-vs-davis",
                Website = entityFinder.WebsiteFinder.GetWebsite(WebsiteName.MMAJunkie),
                Data = GetResourceFile("UFC179.html", "PicksPages")
            };

            Webpage oddsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = false,
                Url = "https://www.bestfightodds.com/events/ufc-179-aldo-vs-mendes-ii-855#",
                Website = entityFinder.WebsiteFinder.GetWebsite(WebsiteName.BestFightOdds),
                Data = GetResourceFile("UFC179.html", "OddsPages")
            };

            context.Webpages.AddRange(new List<Webpage>() { resultsWebpage, picksWebpage, oddsWebpage });
            context.SaveChanges();
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
                Analyst = entityFinder.AnalystFinder.FindAnalyst("Mike Bohn").Result,
                Fight = fight,
                Fighter = winner
            };
            Pick incorrectPick = new Pick(context)
            {
                Analyst = entityFinder.AnalystFinder.FindAnalyst("Dann Stupp").Result,
                Fight = fight,
                Fighter = loser
            };
            context.Picks.AddRange(new List<Pick>() { correctPick, incorrectPick });
            context.SaveChanges();

            Webpage resultsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = true,
                Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                Website = entityFinder.WebsiteFinder.GetWebsite(WebsiteName.Wikipedia),
                Data = GetResourceFile("FN55.html", "ResultsPages")
            };
            Webpage picksWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = true,
                Url = "https://mmajunkie.com/2014/11/ufc-fight-night-55-staff-picks-rockhold-a-unanimous-nod-over-bisping",
                Website = entityFinder.WebsiteFinder.GetWebsite(WebsiteName.MMAJunkie),
                Data = GetResourceFile("FN55.html", "PicksPages")
            };
            Webpage oddsWebpage = new Webpage(context)
            {
                Exhibition = exhibition,
                Parsed = true,
                Url = "https://www.bestfightodds.com/events/ufc-179-aldo-vs-mendes-ii-855#",
                Website = entityFinder.WebsiteFinder.GetWebsite(WebsiteName.BestFightOdds),
                Data = GetResourceFile("FN55.html", "OddsPages")
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

        private static string GetResourceFile(string fileName, string folderName)
        {
            string fileData = "";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string currentAssemblyName = currentAssembly.GetName().Name;
            string basefolderName = "WebsiteHtml";
            using (Stream stream = currentAssembly.GetManifestResourceStream($"{currentAssemblyName}.{basefolderName}.{folderName}.{fileName}"))
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
