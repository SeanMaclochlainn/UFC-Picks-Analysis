using FightData.DataLayer;
using FightData.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
                    ProcessByAnalystXFights(eventObj, 2);
                    ProcessByFightsXAnalyst(eventObj, 3);

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
                while (validXpath)
                {
                    string xPath = GetCorrectXpath(tableXpath, optionalXpaths, wikiDoc, lineNo);
                    if (xPath != "")
                    {
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
                    else
                        validXpath = false;
                }
            }

            void ProcessByAnalystXFights(Event eventObj, int websiteId)
            {
                Webpage webPage = dataUtilities.GetAllWebpages().FirstOrDefault(wp => wp.Event.Id == eventObj.Id && wp.Website.Id == websiteId);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(webPage.Data);

                bool validXpath = true;
                int analystStartPt = 2;
                while (validXpath)
                {
                    //analysts xpath
                    string analystXPath = string.Format("//div[@class='articleBody']/table/tbody/tr[{0}]/td/strong", analystStartPt);
                    
                    HtmlNode analystNode = htmlDoc.DocumentNode.SelectSingleNode(analystXPath);
                    if (analystNode != null)
                    {
                        string analystName = analystNode.InnerText.Trim();
                        Analyst analyst = dataUtilities.GetAllAnalysts().FirstOrDefault(a => a.Name == analystName);

                        int fighterStartPt = 2;
                        foreach (var fight in eventObj.Fights.ToList())
                        {
                            //fighter xpath
                            string fighterXPath = string.Format("//div[@class='articleBody']/table/tbody/tr[{0}]/td[{1}]", analystStartPt, fighterStartPt);
                            HtmlNode fightNode = htmlDoc.DocumentNode.SelectSingleNode(fighterXPath);
                            if (fightNode != null)
                            {
                                string fighterName = fightNode.InnerText.Trim();
                                Fighter fighter = dataUtilities.FindFighter(fighterName, eventObj);
                                Pick pick = new Pick { Analyst = analyst, Fight = fight, FighterPick = fighter };
                                dataUtilities.AddPick(pick);
                                fighterStartPt++;
                            }
                        }
                    }
                    else
                        validXpath = false;

                    analystStartPt++;
                }
            }
            void ProcessByFightsXAnalyst(Event eventObj, int websiteId)
            {
                Webpage webPage = dataUtilities.GetAllWebpages().FirstOrDefault(wp => wp.Event.Id == eventObj.Id && wp.Website.Id == websiteId);
                
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(webPage.Data);

                int fightStartNo = 1;
                int i = 0;
                while(i <= eventObj.Fights.Count)
                {
                    string fightXPath = String.Format("//div[@class='c-entry-content']/p[contains(text(),'Staff picking')][{0}]", fightStartNo);
                    HtmlNode fightNode = htmlDoc.DocumentNode.SelectSingleNode(fightXPath);
                    if (fightNode != null)
                    {
                        string fighterRegex = "(?<=Staff picking ).*?(?=:)";

                        Match fighterMatch = Regex.Match(fightNode.InnerText, fighterRegex);
                        string fighter1Str = fighterMatch.Value;

                        string fighter2Str = fighterMatch.NextMatch().Value;
                        int index = fightNode.InnerText.IndexOf(fighter2Str);
                        string fighter2text = fightNode.InnerText.Substring(index);

                        string fighter1Text = fightNode.InnerText.Substring(0, index);

                        Fighter fighter1 = dataUtilities.FindFighter(fighter1Str, eventObj);
                        Fight fight = dataUtilities.FindFight(fighter1, eventObj);

                        string analystRegex = "(?<=(,|:) ).*?(?=(,|Staff|$))";
                        
                        Match analystMatch = Regex.Match(fighter1Text, analystRegex);
                        
                        while(analystMatch.Success)
                        {
                            string analystStr = analystMatch.Value;
                            Analyst analyst = dataUtilities.GetAnalyst(analystStr);
                            Pick pick = new Pick() { Fight = fight, Analyst = analyst, FighterPick = fighter1 };
                            dataUtilities.AddPick(pick);
                            analystMatch = analystMatch.NextMatch();
                         }

                        Fighter fighter2 = dataUtilities.FindFighter(fighter2Str, eventObj);

                        analystMatch = Regex.Match(fighter2text, analystRegex);
                        while (analystMatch.Success)
                        {
                            string analystStr = analystMatch.Value;
                            Analyst analyst = dataUtilities.GetAnalyst(analystStr);
                            Pick pick = new Pick() { Fight = fight, Analyst = analyst, FighterPick = fighter2 };
                            dataUtilities.AddPick(pick);
                            analystMatch = analystMatch.NextMatch();
                        }
                    }
                    i++;
                    fightStartNo++;
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
