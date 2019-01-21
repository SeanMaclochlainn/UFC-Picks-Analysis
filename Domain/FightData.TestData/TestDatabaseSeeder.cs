using FightData.Domain;
using FightData.Domain.Builders;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FightData.TestData
{
    public class TestDatabaseSeeder
    {
        private FightPicksContext context;
        private EntityFinder entityFinder;
        private EntityUpdater entityUpdater;

        public TestDatabaseSeeder(FightPicksContext context)
        {
            this.context = context;
            entityFinder = new EntityFinder(context);
            entityUpdater = new EntityUpdater(context);
        }

        public void Seed()
        {
            AddWebsite(WebsiteName.Wikipedia, WebsiteType.Result);
            AddWebsite(WebsiteName.MMAJunkie, WebsiteType.Pick);
            AddWebsite(WebsiteName.BloodyElbow, WebsiteType.Pick);
            AddWebsite(WebsiteName.BestFightOdds, WebsiteType.Odds);
            AddPicksPageConfigurations();
            AddAnalyst("Mike Bohn");
            AddAnalyst("Dann Stupp");
            AddFN55();
            AddUFC179();
        }

        private void AddPicksPageConfigurations()
        {
            PicksPageConfiguration mmaJunkieConfiguration = new PicksPageConfigurationBuilder(context)
                .GenerateSampleMmaJunkiePicksPageConfiguration()
                .Build();

            PicksPageConfiguration bloodyElbowConfiguration = new PicksPageConfigurationBuilder(context)
                .GenerateSampleBloodyElbowPicksPageConfiguration()
                .Build();

            entityUpdater.PicksPageConfigurationUpdater.Add(new List<PicksPageConfiguration>() { mmaJunkieConfiguration, bloodyElbowConfiguration });
        }

        private void AddUFC179()
        {
            Exhibition exhibition = new ExhibitionBuilder(context).GenerateExhibition("UFC 179").Build();

            Webpage resultsWebpage = new WebpageBuilder(context)
                .GenerateSampleUnparsedResultsWebpage(exhibition, GetResourceFile("UFC179.html", "WebsiteHtml/ResultsPages"))
                .Build();

            Webpage mmaJunkieWebpage = new WebpageBuilder(context)
                .GenerateSampleUnparsedPicksWebpage(exhibition, GetResourceFile("UFC179.html", "WebsiteHtml/PicksPages/MmaJunkie"), WebsiteName.MMAJunkie)
                .Build();

            Webpage bloodyElbowWebpage = new WebpageBuilder(context)
                .GenerateSampleUnparsedPicksWebpage(exhibition, GetResourceFile("UFC179.html", "WebsiteHtml/PicksPages/BloodyElbow"), WebsiteName.BloodyElbow)
                .Build();

            Webpage oddsWebpage = new WebpageBuilder(context)
                .GenerateSampleUnparsedOddsWebpage(exhibition, GetResourceFile("UFC179.html", "WebsiteHtml/OddsPages"))
                .Build();

            entityUpdater.WebpageUpdater.AddWebpages(new List<Webpage>() { resultsWebpage, mmaJunkieWebpage, bloodyElbowWebpage, oddsWebpage });
        }

        private void AddFN55()
        {
            Exhibition exhibition = new ExhibitionBuilder(context).GenerateExhibition("FN 55").Build();
            Fighter winner = new FighterBuilder(context).GenerateFighter("Luke Rockhold").Build();
            Fighter loser = new FighterBuilder(context).GenerateFighter("Michael Bisping").Build();
            Fight fight = new FightBuilder(context).GenerateFight(exhibition, winner, loser).Build();
            entityUpdater.FightUpdater.AddFight(fight);

            Pick correctPick = new PickBuilder(context)
                .GeneratePick(entityFinder.AnalystFinder.FindAnalyst("Mike Bohn").Result, fight, winner)
                .Build();

            Pick incorrectPick = new PickBuilder(context)
                .GeneratePick(entityFinder.AnalystFinder.FindAnalyst("Dann Stupp").Result, fight, loser)
                .Build();

            entityUpdater.PickUpdater.AddPicks(new List<Pick>() { correctPick, incorrectPick });

            Webpage resultsWebpage = new WebpageBuilder(context)
                .GenerateSampleParsedResultsWebpage(exhibition, GetResourceFile("FN55.html", "WebsiteHtml/ResultsPages"))
                .Build();

            Webpage picksWebpage = new WebpageBuilder(context)
                .GenerateSampleParsedPicksWebpage(exhibition, GetResourceFile("FN55.html", "WebsiteHtml/PicksPages/MmaJunkie"), WebsiteName.MMAJunkie)
                .Build();

            Webpage oddsWebpage = new WebpageBuilder(context)
                .GenerateSampleParsedOddsWebpage(exhibition, GetResourceFile("FN55.html", "WebsiteHtml/OddsPages"))
                .Build();

            entityUpdater.WebpageUpdater.AddWebpages(new List<Webpage>() { resultsWebpage, picksWebpage, oddsWebpage });
        }

        private Analyst AddAnalyst(string name)
        {
            Analyst analyst = new Analyst(context)
            {
                Name = name
            };
            entityUpdater.AnalystUpdater.Add(analyst);
            return analyst;
        }

        private Website AddWebsite(WebsiteName websiteName, WebsiteType websiteType)
        {
            Website website = new Website(context)
            {
                WebsiteName = websiteName,
                WebsiteType = websiteType
            };
            entityUpdater.WebsiteUpdater.Add(website);
            return website;
        }

        private static string GetResourceFile(string fileName, string folderPath)
        {
            string fileData = "";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string currentAssemblyName = currentAssembly.GetName().Name;
            folderPath = folderPath.Replace("/", ".");
            using (Stream stream = currentAssembly.GetManifestResourceStream($"{currentAssemblyName}.{folderPath}.{fileName}"))
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
