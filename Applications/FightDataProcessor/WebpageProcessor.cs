using FightData.DataLayer;
using FightData.Models.DataModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FightDataProcessor
{
    public class WebpageProcessor
    {
        private Event eventObj;
        private DataUtilities dataUtilities;

        public WebpageProcessor(Event eventObj)
        {
            this.eventObj = eventObj;
            dataUtilities = new DataUtilities();
        }

        public void ProcessWebpages()
        {
            ProcessWikipediaEntry();
            ProcessByAnalystXFights(2);
            ProcessByFightsXAnalyst(3);
        }

        private void ProcessWikipediaEntry()
        {
            List<Webpage> webpages = dataUtilities.GetAllWebpages();
            var wikipediaPage = webpages.First(w => w.Event.Id == eventObj.Id && w.Website.WebsiteName == WebsiteName.Wikipedia);
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


                    Fighter fighterAObj = dataUtilities.FindFighter(fighterA, dataUtilities.GetAllFighters());
                    if (fighterAObj == null)
                    {
                        fighterAObj = dataUtilities.PopulateFighterName(fighterA);
                        dataUtilities.AddFighter(fighterAObj);
                    }

                    Fighter fighterBObj = dataUtilities.FindFighter(fighterB, dataUtilities.GetAllFighters());
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

        private void ProcessByAnalystXFights(int websiteId)
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
                    Analyst analyst = dataUtilities.FindAnalyst(analystName);

                    if (analyst == null)
                    {
                        analyst = SelectOrAddAnalyst(analystName);
                    }

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
                            if (fighter == null)
                            {
                                fighter = FindUnknownFighter(fighterName);
                            }
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

        private void ProcessByFightsXAnalyst(int websiteId)
        {
            Webpage webPage = dataUtilities.GetAllWebpages().FirstOrDefault(wp => wp.Event.Id == eventObj.Id && wp.Website.Id == websiteId);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(webPage.Data);

            int fightStartNo = 1;
            int i = 0;
            while (i <= eventObj.Fights.Count)
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
                    if (fighter1 == null)
                    {
                        fighter1 = FindUnknownFighter(fighter1Str);
                    }
                    Fight fight = dataUtilities.FindFight(fighter1, eventObj);

                    string analystRegex = "(?<=(,|:) ).*?(?=(,|Staff|$))";

                    Match analystMatch = Regex.Match(fighter1Text, analystRegex);

                    while (analystMatch.Success)
                    {
                        string analystStr = analystMatch.Value;
                        Analyst analyst = dataUtilities.FindAnalyst(analystStr);
                        if (analyst == null)
                        {
                            analyst = SelectOrAddAnalyst(analystStr);
                        }

                        Pick pick = new Pick() { Fight = fight, Analyst = analyst, FighterPick = fighter1 };
                        dataUtilities.AddPick(pick);
                        analystMatch = analystMatch.NextMatch();
                    }

                    Fighter fighter2 = dataUtilities.FindFighter(fighter2Str, eventObj);
                    if (fighter2 == null)
                    {
                        fighter2 = FindUnknownFighter(fighter2Str);
                    }

                    analystMatch = Regex.Match(fighter2text, analystRegex);
                    while (analystMatch.Success)
                    {
                        string analystStr = analystMatch.Value;
                        Analyst analyst = dataUtilities.FindAnalyst(analystStr);
                        Pick pick = new Pick() { Fight = fight, Analyst = analyst, FighterPick = fighter2 };
                        dataUtilities.AddPick(pick);
                        analystMatch = analystMatch.NextMatch();
                    }
                }
                i++;
                fightStartNo++;
            }
        }

        private Fighter FindUnknownFighter(string name)
        {
            Console.WriteLine("Cannot match fighter {0}. Please select from the list: ", name);
            List<Fighter> fighters = new List<Fighter>();
            eventObj.Fights.ForEach(f => fighters.AddRange(new List<Fighter>() { f.Winner, f.Loser }));
            fighters.ForEach(f => Console.WriteLine("{0}. {1}", fighters.IndexOf(f) + 1, f.FullName));

            int number = 0;
            while (number == 0)
            {
                Console.WriteLine("Enter correct fighter number:");
                string fighterNo = Console.ReadLine();
                int.TryParse(fighterNo, out number);
                if (number == 0 || number > fighters.Count)
                    Console.WriteLine("Incorrect input, please try again");
            }

            number--;
            Fighter fighter = fighters.ElementAt(number);
            dataUtilities.AddAltFighterName(name, fighter);
            return fighter;
        }

        private Analyst SelectOrAddAnalyst(string analystName)
        {
            Console.WriteLine("Cannot find analyst: {0}\n\nPlease select from existing analysts or press n to add add as a new analyst:", analystName);
            List<Analyst> analysts = dataUtilities.GetAllAnalysts();
            analysts.ForEach(a => Console.WriteLine("{0}. {1}", analysts.IndexOf(a) + 1, a.Name));

            Analyst analyst = new Analyst();
            bool validInput = false;
            while (!validInput)
            {
                string analystInput = Console.ReadLine();
                if (analystInput == "n")
                {
                    validInput = true;
                    analyst = dataUtilities.AddAnalyst(analystName);
                }
                else
                {
                    int.TryParse(analystInput, out int number);
                    if (number == 0 || number > analysts.Count)
                    {
                        Console.WriteLine("Incorrect input, please try again");
                        validInput = false;
                    }
                    else
                    {
                        validInput = true;
                        number--;
                        analyst = analysts.ElementAt(number);
                        dataUtilities.AddAnalystAltName(analystName, analyst);
                    }
                }
            }
            return analyst;
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
