using System;
using FightData.DataLayer;
using FightDataProcessor;
using HtmlAgilityPack;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FightData.Models.DataModels;
using System.Linq;
using System.IO;
using System.Reflection;

namespace FightDataProcessorTest
{
    [TestClass]
    public class WebpageProcessorTest
    {
        private static SqliteConnection connection;
        private static DbContextOptions<FightPicksContext> options;
        private static DataUtilities dataUtilities;

        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<FightPicksContext>()
            .UseSqlite(connection)
            .Options;

            using (var context = new FightPicksContext(options))
            {
                context.Database.EnsureCreated();
            }

            dataUtilities = new DataUtilities(options);
            Event eventObj = new Event()
            {
                EventName = "fn 55",
                Fights = new List<Fight>(),
                Webpages = new List<Webpage>()
            };

            string bloodyElbowPageData = GetResourceFile("FN55BloodyElbow.html");
            string wikipediaPage = GetResourceFile("FN55Wikipedia.html");
            string mmaJunkiPage = GetResourceFile("FN55MmaJunkie.html");

            List<Webpage> webpages = new List<Webpage>()
            {
                new Webpage()
                {
                    Event = eventObj,
                    Data = wikipediaPage,
                    Url = "https://en.wikipedia.org/wiki/UFC_Fight_Night:_Rockhold_vs._Bisping",
                    Website = new Website
                    {
                        Id = 1,
                        DomainName = "wikipedia.org",
                        WebsiteName = WebsiteName.Wikipedia
                    }
                },
                new Webpage()
                {
                    Event = eventObj,
                    Data = mmaJunkiPage,
                    Url = "http://mmajunkie.com/2014/11/ufc-fight-night-55-staff-picks-rockhold-a-unanimous-nod-over-bisping",
                    Website = new Website()
                    {
                        Id = 2,
                        DomainName = "mmajunkie.com",
                        WebsiteName = WebsiteName.MMAJunkie
                    }
                },
                new Webpage()
                {
                    Event = eventObj,
                    Data = bloodyElbowPageData,
                    Url = "https://www.bloodyelbow.com/2014/11/6/7171527/ufc-fight-night-bisping-vs-rockhold-staff-picks-and-predictions",
                    Website = new Website
                    {
                        Id = 3,
                        DomainName = "bloodyelbow.com",
                        WebsiteName = WebsiteName.BloodyElbow
                    }
                }
            };

            using (var context = new FightPicksContext(options))
            {
                context.Event.Add(eventObj);
                context.Webpage.AddRange(webpages);
                context.SaveChanges();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            connection.Close();
        }

        [TestMethod]
        public void FirstValidXpathReturned()
        {
            string baseXpath = @"//*[@class='toccolours']";
            List<string> optionalXpaths = new List<string>() { @"/tbody/tr[{0}]", @"/tr[{0}]" };
            HtmlDocument htmlDocument = new HtmlDocument();
            int lineNo = 3;

            Webpage wikiWebpage = dataUtilities.GetAllWebpages().First(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
            htmlDocument.LoadHtml(wikiWebpage.Data);
            string result = WebpageProcessor.GetCorrectXpath(baseXpath, optionalXpaths, htmlDocument, lineNo);

            Assert.AreEqual(@"//*[@class='toccolours']/tr[" + lineNo + "]", result);
        }

        [TestMethod]
        public void AddEvent()
        {
            dataUtilities.AddEvent(new Event
            {
                EventName = "ufc 100",
                Fights = new List<Fight>(),
                Webpages = new List<Webpage>()
            });

            using (var context = new FightPicksContext(options))
            {
                Assert.AreEqual(2, context.Event.Count());
            }
        }

        [TestMethod]
        public void TestWikipediaParsing()
        {
            Event eventObj = dataUtilities.GetAllEvents().First();
            WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities);
            webpageProcessor.ProcessWikipediaEntry();

            using (var context = new FightPicksContext(options))
            {
                Assert.AreEqual(11, context.Fight.Count());
                Assert.AreEqual(22, dataUtilities.GetAllEvents().Single(e => e.Id == eventObj.Id).GetAllFighters().Count);
            }
        }

        [TestMethod]
        public void ProcessByFightsXAnalystTest()
        {
            Event eventObj = dataUtilities.GetAllEvents().First();
            List<string> inputs = new List<string>()
            {
                "n","n","n","n","n","n","n","n","n","n","n"
            };
            TestInputReceiver inputReceiver = new TestInputReceiver(inputs);
            WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities, inputReceiver);

            webpageProcessor.ProcessWikipediaEntry();
            webpageProcessor.ProcessByFightsXAnalyst(3);

            using (var context = new FightPicksContext(options))
            {
                List<Pick> picks = context.Pick
                    .Include(p => p.Fight)
                    .ThenInclude(f => f.Winner)
                    .Include(p => p.Fight)
                    .ThenInclude(f => f.Event)
                    .Include(p => p.Analyst)
                    .Include(p => p.FighterPick)
                    .ToList()
                    .Where(p => p.Event.Id == eventObj.Id)
                    .ToList();

                Assert.AreEqual("Anton", picks.Single(p => p.FighterPick.LastName == "Bisping").Analyst.Name);
                Assert.AreEqual(10, picks.Count(p => p.Fight.Winner.LastName == "Rockhold" && p.FighterPick.LastName == "Rockhold"));
            }
        }

        [TestMethod]
        public void ProcessByAnalystXFightsTest()
        {
            Event eventObj = dataUtilities.GetAllEvents().First();
            List<string> inputs = new List<string>()
            {
                "n","n","n","n","n","n","n","n"
            };
            TestInputReceiver inputReceiver = new TestInputReceiver(inputs);
            WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities, inputReceiver);

            webpageProcessor.ProcessWikipediaEntry();
            webpageProcessor.ProcessByAnalystXFights(2);

            using (var context = new FightPicksContext(options))
            {
                List<Pick> picks = context.Pick
                    .Include(p => p.Fight)
                    .ThenInclude(f => f.Winner)
                    .Include(p => p.Fight)
                    .ThenInclude(f => f.Event)
                    .Include(p => p.Analyst)
                    .Include(p => p.FighterPick)
                    .Include(p=>p.Analyst)
                    .ThenInclude(a=>a.Website)
                    .ToList()
                    .Where(p => p.Event.Id == eventObj.Id && p.Analyst.Website.WebsiteName == WebsiteName.MMAJunkie)
                    .ToList();

                var test = picks.Where(p => p.FighterPick.LastName == "Whittaker");
                Assert.AreEqual("Matt Erickson", picks.Single(p => p.FighterPick.LastName == "Whittaker").Analyst.Name);
                Assert.AreEqual(2, picks.Count(p => p.Fight.Winner.LastName == "Iaquinta" && p.FighterPick.LastName == "Iaquinta"));
            }

        }

        private static string GetResourceFile(string fileName)
        {
            string fileData = "";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string folderName = "WebsiteData";
            using (Stream stream = currentAssembly.GetManifestResourceStream(String.Format("{0}.{1}.{2}", currentAssembly.GetName().Name, folderName, fileName)))
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
