using FightData.DataLayer;
using FightData.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FightDataProcessor
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();
            string connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            Console.WriteLine("1: Collect Data\n2: Process Events");
            var input = Console.ReadLine();
            DataUtilities utilities = new DataUtilities(connectionString);
            if (input == "1")
            {
                while (true)
                {
                    Console.WriteLine("Enter the event name or press e to select existing event");
                    string eventSelection = Console.ReadLine();
                    int eventId = 0;
                    if (eventSelection == "e")
                    {
                        var eventList = utilities.GetAllEvents();
                        Console.WriteLine("Select event ID:");
                        foreach (var eventObj in eventList)
                        {
                            Console.WriteLine("ID: {0} Name: {1}", eventObj.Id, eventObj.EventName);
                        }
                        eventId = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        eventId = utilities.AddEvent(eventSelection);
                    }

                    List<Website> websites = utilities.GetAllWebsites();
                    HttpClient client = new HttpClient();

                    //WebClient client = new WebClient();
                    //client.Headers.Add("User-Agent: Other");
                    //client.Headers.Add("user-agent", "Only a test!");
                    //client.Encoding = Encoding.UTF8;
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    foreach (var website in websites)
                    {
                        Webpage webpage = new Webpage();
                        Console.WriteLine("Enter {0} url (Enter to skip website)", website.Name);
                        string websiteUrl = Console.ReadLine();
                        if (!string.IsNullOrEmpty(websiteUrl))
                        {
                            webpage.URL = websiteUrl;
                            webpage.UFCEvent = new Event() { Id = eventId };
                            webpage.Website = website;



                            Task<HttpResponseMessage> result = client.GetAsync(websiteUrl);
                            while (!result.IsCompleted)
                            {

                            }
                            HttpResponseMessage result2 = result.Result;
                            HttpContent content = result2.Content;
                            Task<string> contentstr = content.ReadAsStringAsync();
                            while (!contentstr.IsCompleted)
                            {

                            }
                            webpage.Data = contentstr.Result;
                            //webpage.Data = client.DownloadString(websiteUrl);
                            utilities.AddWebPage(webpage);
                        }
                    }
                }
            }
            else if (input == "2")
            {
                var events = utilities.GetAllEvents();
                foreach (var eventObj in events)
                {
                    //get fights
                    //todo: better way to store website id
                    var wikipediaPage = utilities.GetWebPage(eventObj.Id, 1);
                    List<Fighter> allFighters = utilities.GetAllFighters();
                    List<AltName> altNames = utilities.GetAltNames();
                    List<Fight> allFights = utilities.GetAllfights();
                    List<Pick> allPicks = utilities.GetAllPicks();
                    var wikiDoc = new HtmlDocument();
                    wikiDoc.LoadHtml(wikipediaPage.Data);

                    string tableXpath = @"//*[@class='toccolours']";

                    List<string> optionalXpaths = new List<string>() { @"/tbody/tr[{0}]", @"/tr[{0}]" };

                    string tableRows = @"/tbody/tr[{0}]|";
                    wikiDoc.DocumentNode.SelectSingleNode(tableXpath);

                    int i = 1;
                    bool validXpath = true;
                    while (true)
                    {
                        string xPath = GetCorrectXpath(tableXpath, optionalXpaths, wikiDoc, i);
                        if (xPath == "")
                            break;
                        var node = wikiDoc.DocumentNode.SelectSingleNode(xPath);
                        int cardType = 0;
                        if (node.InnerText.Contains("Main Card"))
                        {
                            cardType = 1;
                            i++;
                            continue;
                        }
                        else if (node.InnerText.Contains("Preliminary Card"))
                        {
                            cardType = 2;
                            i++;
                            continue;
                        }
                        else if (node.InnerText.Contains("Weight class"))
                        {
                            i++;
                            continue;
                        }
                        string fighterAXpath = xPath+@"/td[2]";
                        string fighterBXpath = xPath+@"/td[4]";
                        
                        string fighterA = wikiDoc.DocumentNode.SelectSingleNode(fighterAXpath).InnerText;
                        string fighterB = wikiDoc.DocumentNode.SelectSingleNode(fighterBXpath).InnerText;

                        Fighter fighterAObj = utilities.FindFighter(fighterA, allFighters, altNames);
                        if (fighterAObj == null)
                        {
                            Fighter fightera = utilities.PopulateFighterName(fighterA);
                            fighterAObj = new Fighter();
                            fighterAObj.Id = utilities.AddFighter(fightera);
                        }

                        Fighter fighterBObj = utilities.FindFighter(fighterB, allFighters, altNames);
                        if (fighterBObj == null)
                        {
                            Fighter fighterb = utilities.PopulateFighterName(fighterB);
                            fighterBObj = new Fighter();
                            fighterBObj.Id = utilities.AddFighter(fighterb);
                        }

                        Fight fight = new Fight { Event = eventObj, FighterA = fighterAObj, FighterB = fighterBObj, Winner = fighterAObj, CardTypeId = cardType };
                        if (!allFights.Contains(fight))
                            utilities.AddFight(fight);
                        eventObj.Fights = new List<Fight>() { fight };
                        i++;
                    }

                    //get mma junkie picks
                    Webpage mmaJunkiePage = utilities.GetWebPage(eventObj.Id, 2);
                    HtmlDocument mmaJunkieDoc = new HtmlDocument();
                    mmaJunkieDoc.LoadHtml(mmaJunkiePage.Data);
                    var container = mmaJunkieDoc.GetElementbyId("content-container");
                    string picksTable = mmaJunkieDoc.DocumentNode.SelectSingleNode("//div[@class='articleBody']/table").InnerText;
                    List<string> analystPicksTable = picksTable.Split(new string[] { "\n\n\n" }, StringSplitOptions.None).ToList();
                    //analystPicksTable.RemoveRange(0, 2);
                    analystPicksTable.RemoveAll(ap => !ap.Contains("@"));
                    List<Analyst> analysts = utilities.GetAllAnalysts();
                    foreach (var analystPicks in analystPicksTable)
                    {
                        List<string> analystPicksList = analystPicks.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
                        analystPicksList.RemoveAll(p => p == "");
                        string analystName = analystPicksList[0];
                        if (analystName.Contains("@"))
                        {
                            analystName = analystName.Remove(analystName.IndexOf("@"));
                            analystName = analystName.Trim();
                        }
                        Analyst analyst = analysts.SingleOrDefault(a => a.Name == analystName);
                        if (analyst == null)
                        {
                            Console.WriteLine("Couldn't find analyst {0}", analystName);
                            Console.ReadLine();
                            continue;
                        }
                        analystPicksList.RemoveRange(0, analystPicksList.Count - eventObj.Fights.Count);
                        analystPicksList.RemoveRange(0, 3);
                        analystPicksList.RemoveAll(ap => ap.Contains("2014 Champion"));
                        for (int fightCount = 0; fightCount < analystPicksList.Count - 1; fightCount++)
                        {
                            Fight fight = eventObj.Fights[fightCount];
                            string analystPick = analystPicksList[fightCount];
                            Fighter fighter = utilities.GetAnalystsFighterPick(fight, analystPicksList[fightCount], altNames);
                            bool correct = fight.Winner.Id == fighter.Id ? true : false;
                            Pick pick = new Pick() { AnalystId = analyst.Id, FightId = fight.Id, FighterPickId = fighter.Id, Correct = correct };
                            if (!allPicks.Any(p => p.FightId == pick.FightId && p.AnalystId == analyst.Id))
                                utilities.AddPick(pick.AnalystId, analystPick, pick.Correct, pick.FightId, pick.FighterPickId);

                        }
                    }
                }
            }
        }

        private static string GetCorrectXpath(string baseXpath, List<string> optionalXpaths, HtmlDocument document, int formatNo)
        {
            string xPath = "";
            foreach(var optxPath in optionalXpaths)
            {
                string optxPathForm = string.Format(optxPath, formatNo);
                xPath = baseXpath + optxPathForm;
                HtmlNode htmlNode = document.DocumentNode.SelectSingleNode(xPath);
                if (htmlNode != null)
                    break;
                else
                    xPath = "";
            }
            return xPath;

        }
    }
}
