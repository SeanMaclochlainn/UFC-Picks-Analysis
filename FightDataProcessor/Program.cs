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
            FightPicksContext db = new FightPicksContext();

            DataUtilities dataUtilities = new DataUtilities();

            //Configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", false, true)
            //.Build();
            //string connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            Console.WriteLine("1: Collect Data\n2: Process Events");
            var input = Console.ReadLine();
            if (input == "1")
            {
                while (true)
                {
                    Console.WriteLine("Enter the event name or press e to select existing event");
                    string eventSelection = Console.ReadLine();
                    int eventId = 0;
                    Event eventObj = null;
                    if (eventSelection == "e")
                    {
                        List<Event> eventList = dataUtilities.GetAllEvents();
                        Console.WriteLine("Select event ID:");
                        foreach (var existingEvent in eventList)
                        {
                            Console.WriteLine("ID: {0} Name: {1}", existingEvent.Id, existingEvent.EventName);
                        }
                        eventId = int.Parse(Console.ReadLine());
                        eventObj = eventList.First(e => e.Id == eventId);
                    }
                    else
                    {
                        eventObj = new Event() { EventName = eventSelection };
                        dataUtilities.AddEvent(eventObj);
                    }

                    List<Website> websites = dataUtilities.GetAllWebsites();
                    HttpClient client = new HttpClient();
                    foreach (var website in websites)
                    {
                        Webpage webpage = new Webpage();
                        Console.WriteLine("Enter {0} url (Enter to skip website)", website.Name);
                        string websiteUrl = Console.ReadLine();
                        if (!string.IsNullOrEmpty(websiteUrl))
                        {
                            webpage.Url = websiteUrl;
                            webpage.Event = eventObj;
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
                            dataUtilities.AddWebpage(webpage);
                        }
                    }
                }
            }
            else if (input == "2")
            {
                var events = dataUtilities.GetAllEvents();
                foreach (var eventObj in events)
                {
                    ProcessWikipediaEntry(eventObj);
                    //get fights
                    //todo: make websites enums
                    //var wikipediaPage = db.Webpage.First(w => w.Event.Id == eventObj.Id && w.Website.Name.Contains("Wikipedia"));
                    //List<Fighter> allFighters = db.Fighter.ToList();
                    //List<AltName> altNames = db.AltName.ToList();
                    //List<Fight> allFights = db.Fight.ToList();
                    //List<Pick> allPicks = db.Pick.ToList();
                    //var wikiDoc = new HtmlDocument();
                    //wikiDoc.LoadHtml(wikipediaPage.Data);

                    //string tableXpath = @"//*[@class='toccolours']";

                    //List<string> optionalXpaths = new List<string>() { @"/tbody/tr[{0}]", @"/tr[{0}]" };

                    //string tableRows = @"/tbody/tr[{0}]|";
                    //wikiDoc.DocumentNode.SelectSingleNode(tableXpath);

                    //int i = 1;
                    //bool validXpath = true;
                    //while (true)
                    //{
                    //    string xPath = GetCorrectXpath(tableXpath, optionalXpaths, wikiDoc, i);
                    //    if (xPath == "")
                    //        break;
                    //    var node = wikiDoc.DocumentNode.SelectSingleNode(xPath);
                    //    int cardType = 0;
                    //    if (node.InnerText.Contains("Main Card"))
                    //    {
                    //        cardType = 1;
                    //        i++;
                    //        continue;
                    //    }
                    //    else if (node.InnerText.Contains("Preliminary Card"))
                    //    {
                    //        cardType = 2;
                    //        i++;
                    //        continue;
                    //    }
                    //    else if (node.InnerText.Contains("Weight class"))
                    //    {
                    //        i++;
                    //        continue;
                    //    }
                    //    string fighterAXpath = xPath + @"/td[2]";
                    //    string fighterBXpath = xPath + @"/td[4]";

                    //    string fighterA = wikiDoc.DocumentNode.SelectSingleNode(fighterAXpath).InnerText;
                    //    string fighterB = wikiDoc.DocumentNode.SelectSingleNode(fighterBXpath).InnerText;

                    //Fighter fighterAObj = db.Fighter.First(f => f.FullName == fighterA);//utilities.FindFighter(fighterA, allFighters, altNames);
                    //if (fighterAObj == null)
                    //{
                    //    var names = fighterA.Split(new String[] { " " }, StringSplitOptions.None).ToList();

                    //    Fighter fightera = utilities.PopulateFighterName(fighterA);
                    //    fighterAObj = new Fighter();
                    //    fighterAObj.Id = utilities.AddFighter(fightera);
                    //}

                    //Fighter fighterBObj = utilities.FindFighter(fighterB, allFighters, altNames);
                    //if (fighterBObj == null)
                    //{
                    //    Fighter fighterb = utilities.PopulateFighterName(fighterB);
                    //    fighterBObj = new Fighter();
                    //    fighterBObj.Id = utilities.AddFighter(fighterb);
                    //}

                    //Fight fight = new Fight { Event = eventObj, FighterA = fighterAObj, FighterB = fighterBObj, Winner = fighterAObj, CardTypeId = cardType };
                    //if (!allFights.Contains(fight))
                    //    utilities.AddFight(fight);
                    //eventObj.Fights = new List<Fight>() { fight };
                    //i++;
                    //}

                    //        //get mma junkie picks
                    //        Webpage mmaJunkiePage = utilities.GetWebPage(eventObj.Id, 2);
                    //        HtmlDocument mmaJunkieDoc = new HtmlDocument();
                    //        mmaJunkieDoc.LoadHtml(mmaJunkiePage.Data);
                    //        var container = mmaJunkieDoc.GetElementbyId("content-container");
                    //        string picksTable = mmaJunkieDoc.DocumentNode.SelectSingleNode("//div[@class='articleBody']/table").InnerText;
                    //        List<string> analystPicksTable = picksTable.Split(new string[] { "\n\n\n" }, StringSplitOptions.None).ToList();
                    //        //analystPicksTable.RemoveRange(0, 2);
                    //        analystPicksTable.RemoveAll(ap => !ap.Contains("@"));
                    //        List<Analyst> analysts = utilities.GetAllAnalysts();
                    //        foreach (var analystPicks in analystPicksTable)
                    //        {
                    //            List<string> analystPicksList = analystPicks.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
                    //            analystPicksList.RemoveAll(p => p == "");
                    //            string analystName = analystPicksList[0];
                    //            if (analystName.Contains("@"))
                    //            {
                    //                analystName = analystName.Remove(analystName.IndexOf("@"));
                    //                analystName = analystName.Trim();
                    //            }
                    //            Analyst analyst = analysts.SingleOrDefault(a => a.Name == analystName);
                    //            if (analyst == null)
                    //            {
                    //                Console.WriteLine("Couldn't find analyst {0}", analystName);
                    //                Console.ReadLine();
                    //                continue;
                    //            }
                    //            analystPicksList.RemoveRange(0, analystPicksList.Count - eventObj.Fights.Count);
                    //            analystPicksList.RemoveRange(0, 3);
                    //            analystPicksList.RemoveAll(ap => ap.Contains("2014 Champion"));
                    //            for (int fightCount = 0; fightCount < analystPicksList.Count - 1; fightCount++)
                    //            {
                    //                Fight fight = eventObj.Fights[fightCount];
                    //                string analystPick = analystPicksList[fightCount];
                    //                Fighter fighter = utilities.GetAnalystsFighterPick(fight, analystPicksList[fightCount], altNames);
                    //                bool correct = fight.Winner.Id == fighter.Id ? true : false;
                    //                Pick pick = new Pick() { AnalystId = analyst.Id, FightId = fight.Id, FighterPickId = fighter.Id, Correct = correct };
                    //                if (!allPicks.Any(p => p.FightId == pick.FightId && p.AnalystId == analyst.Id))
                    //                    utilities.AddPick(pick.AnalystId, analystPick, pick.Correct, pick.FightId, pick.FighterPickId);

                    //            }
                    //        }
                    //    }
                    //}
                }
            }

            void ProcessWikipediaEntry(Event eventObj)
            {
                List<Webpage> webpages = dataUtilities.GetAllWebpages();
                var wikipediaPage = webpages.First(w => w.Event.Id == eventObj.Id && w.Website.Name.Contains("Wikipedia"));
                var wikiDoc = new HtmlDocument();
                wikiDoc.LoadHtml(wikipediaPage.Data);

                string tableXpath = @"//*[@class='toccolours']";

                List<string> optionalXpaths = new List<string>() { @"/tbody/tr[{0}]", @"/tr[{0}]" };

                string tableRows = @"/tbody/tr[{0}]|";
                wikiDoc.DocumentNode.SelectSingleNode(tableXpath);

                int lineNo = 1;
                bool validXpath = true;
                CardType cardType = new CardType();
                while (true)
                {
                    string xPath = GetCorrectXpath(tableXpath, optionalXpaths, wikiDoc, lineNo);
                    if (xPath == "")
                        break;
                    var node = wikiDoc.DocumentNode.SelectSingleNode(xPath);

                    string mainCard = "main card";
                    string preliminaryCard = "preliminary card";
                    string weightClass = "weight class";
                    if (node.InnerText.ToLower().Contains(mainCard))
                    {
                        cardType = dataUtilities.GetCardType(mainCard);
                        lineNo++;
                        continue;
                    }
                    else if (node.InnerText.ToLower().Contains(preliminaryCard))
                    {
                        cardType = dataUtilities.GetCardType(preliminaryCard);
                        lineNo++;
                        continue;
                    }
                    else if (node.InnerText.ToLower().Contains(weightClass))
                    {
                        lineNo++;
                        continue;
                    }
                    string fighterAXpath = xPath + @"/td[2]";
                    string fighterBXpath = xPath + @"/td[4]";

                    string fighterA = wikiDoc.DocumentNode.SelectSingleNode(fighterAXpath).InnerText;
                    string fighterB = wikiDoc.DocumentNode.SelectSingleNode(fighterBXpath).InnerText;


                    Fighter fighterAObj = dataUtilities.FindFighter(fighterA);
                    if (fighterAObj == null)
                    {
                        fighterAObj = dataUtilities.PopulateFighterName(fighterA);
                        dataUtilities.AddFighter(fighterAObj);
                    }

                    Fighter fighterBObj = dataUtilities.FindFighter(fighterB);
                    if (fighterBObj == null)
                    {
                        fighterBObj = dataUtilities.PopulateFighterName(fighterB);
                        dataUtilities.AddFighter(fighterBObj);
                    }

                    Fight fight = new Fight { Event = eventObj, Winner = fighterAObj, Loser = fighterBObj, CardType = cardType };
                    if (!dataUtilities.GetAllFights().Contains(fight))
                        dataUtilities.AddFight(fight);
                    lineNo++;
                }
            }
        }

        private static string GetCorrectXpath(string baseXpath, List<string> optionalXpaths, HtmlDocument document, int formatNo)
        {
            string xPath = "";
            foreach (var optxPath in optionalXpaths)
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
