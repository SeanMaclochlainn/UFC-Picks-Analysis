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
            Fight fight1 = new FightBuilder(context).GenerateFight(exhibition, "Luke Rockhold", "Michael Bisping").Build();
            Fight fight2 = new FightBuilder(context).GenerateFight(exhibition, "Al Iaquinta", "Ross Pearson").Build();
            Fight fight3 = new FightBuilder(context).GenerateFight(exhibition, "Robert Whittaker", "Clint Hester").Build();
            entityUpdater.FightUpdater.AddFights(new List<Fight>() { fight1, fight2, fight3 });

            Analyst mikeBohn = entityFinder.AnalystFinder.FindAnalyst("Mike Bohn").Result;
            Analyst dannStupp = entityFinder.AnalystFinder.FindAnalyst("Dann Stupp").Result;
            Pick fight1CorrectPick = new PickBuilder(context).GeneratePick(mikeBohn, fight1, fight1.Winner).Build();
            Pick fight1IncorrectPick = new PickBuilder(context).GeneratePick(dannStupp, fight1, fight1.Loser).Build();
            Pick fight2MikeBohn = new PickBuilder(context).GeneratePick(mikeBohn, fight2, fight2.Loser).Build();
            Pick fight3MikeBohn = new PickBuilder(context).GeneratePick(mikeBohn, fight3, fight3.Loser).Build();
            Odd fight1WinnerOdds = new OddsBuilder(context).GenerateOdd(1.18M, fight1.Winner, fight1).Build();
            Odd fight1LoserOdds = new OddsBuilder(context).GenerateOdd(5.30M, fight1.Loser, fight1).Build();
            Odd fight2WinnerOdds = new OddsBuilder(context).GenerateOdd(2.10M, fight2.Winner, fight2).Build();
            Odd fight2LoserOdds = new OddsBuilder(context).GenerateOdd(1.77M, fight2.Loser, fight2).Build();
            Odd fight3WinnerOdds = new OddsBuilder(context).GenerateOdd(2.58M, fight3.Winner, fight3).Build();
            Odd fight3LoserOdds = new OddsBuilder(context).GenerateOdd(1.54M, fight3.Loser, fight3).Build();
            entityUpdater.OddUpdater.AddOdds(new List<Odd>() { fight1WinnerOdds, fight1LoserOdds, fight2WinnerOdds, fight2LoserOdds, fight3WinnerOdds, fight3LoserOdds });

            entityUpdater.PickUpdater.AddPicks(new List<Pick>() { fight1CorrectPick, fight1IncorrectPick, fight2MikeBohn, fight3MikeBohn });

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
