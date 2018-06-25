using System;
using FightData.Domain;
using FightDataProcessor;
using HtmlAgilityPack;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace FightDataProcessorTest
{
    [TestClass]
    public class WebpageProcessorTest
    {
        //private SqliteConnection connection;
        //private DbContextOptions<FightPicksContext> options;
        //private DataUtilities dataUtilities;
        //private static TestSuiteSetup testSuiteSetup;

        //[AssemblyInitialize()]
        //public static void AssemblyInitialize(TestContext tc)
        //{
        //    testSuiteSetup = new TestSuiteSetup();
        //}

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();

        //    options = new DbContextOptionsBuilder<FightPicksContext>()
        //        .UseSqlite(connection)
        //        .Options;

        //    using (var context = new FightPicksContext(options))
        //    {
        //        context.Database.EnsureCreated();
        //    }

        //    dataUtilities = new DataUtilities(options);
        //    List<UfcEvent> events = new List<UfcEvent>()
        //    {
        //        new UfcEvent()
        //        {
        //            EventName = "fn 55",
        //            Fights = new List<Fight>(),
        //            Webpages = new List<Webpage>()
        //        },
        //        new UfcEvent()
        //        {
        //            EventName = "fn 56",
        //            Fights = new List<Fight>(),
        //            Webpages = new List<Webpage>()
        //        }
        //    };

        //    Website wikipediaWebsite = new Website()
        //    {
        //        Id = 1,
        //        DomainName = "wikipedia.org",
        //        WebsiteName = WebsiteName.Wikipedia
        //    };

        //    Website mmajunkieWebsite = new Website()
        //    {
        //        Id = 2,
        //        DomainName = "mmajunkie.com",
        //        WebsiteName = WebsiteName.MMAJunkie
        //    };

        //    Website bloodyElbowWebsite = new Website()
        //    {
        //        Id = 3,
        //        DomainName = "bloodyelbow.com",
        //        WebsiteName = WebsiteName.BloodyElbow
        //    };


        //    List<Webpage> webpages = new List<Webpage>()
        //    {
        //        new Webpage()
        //        {
        //            Event = events.ElementAt(0),
        //            Data = GetResourceFile("FN55Wikipedia.html"),
        //            Website = wikipediaWebsite,
        //            Url = ""
        //        },
        //        new Webpage()
        //        {
        //            Event = events.ElementAt(0),
        //            Data = GetResourceFile("FN55MmaJunkie.html"),
        //            Website = mmajunkieWebsite,
        //            Url = ""
        //        },
        //        new Webpage()
        //        {
        //            Event = events.ElementAt(0),
        //            Data = GetResourceFile("FN55BloodyElbow.html"),
        //            Website = bloodyElbowWebsite,
        //            Url = ""
        //        },
        //        new Webpage()
        //        {
        //            Event = events.ElementAt(1),
        //            Data = GetResourceFile("FN56BloodyElbow.html"),
        //            Website = bloodyElbowWebsite,
        //            Url = ""
        //        },
        //        new Webpage()
        //        {
        //            Event = events.ElementAt(1),
        //            Data = GetResourceFile("FN56Wikipedia.html"),
        //            Website = wikipediaWebsite,
        //            Url = ""

        //        },
        //        new Webpage()
        //        {
        //            Event = events.ElementAt(1),
        //            Data = GetResourceFile("FN56MMAJunkie.html"),
        //            Website = mmajunkieWebsite,
        //            Url = ""

        //        }
        //    };

        //    using (var context = new FightPicksContext(options))
        //    {
        //        context.Events.AddRange(events);
        //        context.Webpages.AddRange(webpages);
        //        context.SaveChanges();
        //    }
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    connection.Close();
        //}

        //[TestMethod]
        //public void FirstValidXpathReturned()
        //{
        //    string baseXpath = @"//*[@class='toccolours']";
        //    List<string> optionalXpaths = new List<string>() { @"/tbody/tr[{0}]", @"/tr[{0}]" };
        //    HtmlDocument htmlDocument = new HtmlDocument();
        //    int lineNo = 3;

        //    Webpage wikiWebpage = dataUtilities.GetAllWebpages().First(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        //    htmlDocument.LoadHtml(wikiWebpage.Data);
        //    string result = WebpageProcessor.GetCorrectXpath(baseXpath, optionalXpaths, htmlDocument, lineNo);

        //    Assert.AreEqual(@"//*[@class='toccolours']/tr[" + lineNo + "]", result);
        //}

        //[TestMethod]
        //public void AddEvent()
        //{
        //    dataUtilities.AddEvent(new UfcEvent
        //    {
        //        EventName = "ufc xyz",
        //        Fights = new List<Fight>(),
        //        Webpages = new List<Webpage>()
        //    });

        //    using (var context = new FightPicksContext(options))
        //    {
        //        Assert.AreEqual(1, context.Events.Count(e => e.EventName == "ufc xyz"));
        //    }
        //}

        //[TestMethod]
        //public void TestWikipediaParsing()
        //{
        //    UfcEvent eventObj = dataUtilities.GetAllEvents().First();
        //    WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities);
        //    webpageProcessor.ProcessWikipediaEntry();

        //    using (var context = new FightPicksContext(options))
        //    {
        //        Assert.AreEqual(11, context.Fights.Count());
        //        Assert.AreEqual(22, dataUtilities.GetAllEvents().Single(e => e.Id == eventObj.Id).GetAllFighters().Count);
        //    }
        //}

        //[TestMethod]
        //public void ProcessByFightsXAnalystTest()
        //{
        //    UfcEvent eventObj = dataUtilities.GetAllEvents().First();
        //    List<string> inputs = new List<string>()
        //    {
        //        "n","n","n","n","n","n","n","n","n","n","n"
        //    };
        //    TestUI dataProcessorUI = new TestUI(inputs, testSuiteSetup.Configuration);
        //    WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities, dataProcessorUI);

        //    webpageProcessor.ProcessWikipediaEntry();
        //    webpageProcessor.ProcessByFightsXAnalyst(3);

        //    using (var context = new FightPicksContext(options))
        //    {
        //        List<Pick> picks = context.Picks
        //            .Include(p => p.Fight)
        //            .ThenInclude(f => f.Winner)
        //            .Include(p => p.Fight)
        //            .ThenInclude(f => f.Event)
        //            .Include(p => p.Analyst)
        //            .ThenInclude(a => a.Website)
        //            .Include(p => p.FighterPick)
        //            .ToList()
        //            .Where(p => p.Event.Id == eventObj.Id)
        //            .ToList();

        //        Assert.AreEqual("Anton", picks.Single(p => p.FighterPick.LastName == "Bisping").Analyst.Name);
        //        Assert.AreEqual(10, picks.Count(p => p.Fight.Winner.LastName == "Rockhold" && p.FighterPick.LastName == "Rockhold" && p.Analyst.Website.WebsiteName == WebsiteName.BloodyElbow));
        //    }
        //}

        //[TestMethod]
        //public void ProcessByAnalystXFightsTest()
        //{
        //    UfcEvent eventObj = dataUtilities.GetAllEvents().First();
        //    List<string> inputs = new List<string>()
        //    {
        //        "n","n","n","n","n","n","n","n"
        //    };
        //    TestUI dataProcessorUi = new TestUI(inputs, testSuiteSetup.Configuration);
        //    WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities, dataProcessorUi);

        //    webpageProcessor.ProcessWikipediaEntry();
        //    webpageProcessor.ProcessByAnalystXFights(2);

        //    using (var context = new FightPicksContext(options))
        //    {
        //        List<Pick> picks = context.Picks
        //            .Include(p => p.Fight)
        //            .ThenInclude(f => f.Winner)
        //            .Include(p => p.Fight)
        //            .ThenInclude(f => f.Event)
        //            .Include(p => p.Analyst)
        //            .Include(p => p.FighterPick)
        //            .Include(p => p.Analyst)
        //            .ThenInclude(a => a.Website)
        //            .ToList()
        //            .Where(p => p.Event.Id == eventObj.Id && p.Analyst.Website.WebsiteName == WebsiteName.MMAJunkie)
        //            .ToList();

        //        Assert.AreEqual("Matt Erickson", picks.Single(p => p.FighterPick.LastName == "Whittaker").Analyst.Name);
        //        Assert.AreEqual(2, picks.Count(p => p.Fight.Winner.LastName == "Iaquinta" && p.FighterPick.LastName == "Iaquinta"));
        //    }

        //}

        //[TestMethod]
        //public void DuplicateSurnamesTest()
        //{
        //    UfcEvent eventObj = dataUtilities.GetAllEvents().Single(e => e.EventName.ToLower() == "fn 56");
        //    List<string> inputs = new List<string>()
        //    {
        //        "n","w","n","w","n","n","1","n","n","n","n","n","n","n","n","n","n","1","n","n","n","n","13"
        //    };
        //    TestUI dataProcessorUi = new TestUI(inputs, testSuiteSetup.Configuration);
        //    WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities, dataProcessorUi);

        //    webpageProcessor.ProcessWebpages();

        //    using (var context = new FightPicksContext(options))
        //    {
        //        List<Pick> picks = context.Picks
        //            .Include(p => p.Fight)
        //            .ThenInclude(f => f.Winner)
        //            .Include(p => p.Fight)
        //            .ThenInclude(f => f.Event)
        //            .Include(p => p.Analyst)
        //            .Include(p => p.FighterPick)
        //            .Include(p => p.Analyst)
        //            .ThenInclude(a => a.Website)
        //            .ToList()
        //            .Where(p => p.Event.Id == eventObj.Id && p.Analyst.Website.WebsiteName == WebsiteName.MMAJunkie)
        //            .ToList();
        //        Assert.AreEqual(1,
        //            picks.Count(p =>
        //                p.Analyst.Name == "Mike Bohn" &&
        //                p.Fight.Winner.LastName == "Alves" &&
        //                p.FighterPick.LastName == "Alves"));

        //        Assert.AreEqual(0, picks.Count(p=>p.Fight.Winner.LastName == "Silva"));
        //    }

        //}

        //private static string GetResourceFile(string fileName)
        //{
        //    string fileData = "";
        //    Assembly currentAssembly = Assembly.GetExecutingAssembly();
        //    string folderName = "WebsiteData";
        //    using (Stream stream = currentAssembly.GetManifestResourceStream(String.Format("{0}.{1}.{2}", currentAssembly.GetName().Name, folderName, fileName)))
        //    {
        //        using (StreamReader sr = new StreamReader(stream))
        //        {
        //            fileData = sr.ReadToEnd();
        //        }
        //    }
        //    return fileData;
        //}
    }
}
