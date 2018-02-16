using FightData.DataLayer;
using FightData.Models.DataModels;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
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
        private DataProcessorUI dataProcessorUI;

        public WebpageProcessor(Event eventObj, DataUtilities dataUtilities)
            : this(eventObj, dataUtilities, new DataProcessorUI())
        {
        }

        public WebpageProcessor(Event eventObj, DataUtilities dataUtilities, DataProcessorUI dataProcessorUI)
        {
            this.eventObj = eventObj;
            this.dataUtilities = dataUtilities;
            this.dataProcessorUI = dataProcessorUI;
        }

        public void ProcessWebpages()
        {
            ProcessWikipediaEntry();
            ProcessByAnalystXFights(2);
            ProcessByFightsXAnalyst(3);
        }

        public void ProcessWikipediaEntry()
        {
            List<Webpage> webpages = dataUtilities.GetAllWebpages();
            Webpage wikipediaPage = eventObj.Webpages.First(w => w.Event.Id == eventObj.Id && w.Website.WebsiteName == WebsiteName.Wikipedia);
            HtmlDocument wikiDoc = new HtmlDocument();
            wikiDoc.LoadHtml(wikipediaPage.Data);

            string tableXpath = @"//*[@class='toccolours']";

            List<string> optionalXpaths = new List<string>() { @"/tbody/tr[{0}]", @"/tr[{0}]" };

            int lineNo = 1;
            bool validXpath = true;
            CardType cardType = new CardType();
            while (validXpath)
            {
                string xPath = GetCorrectXpath(tableXpath, optionalXpaths, wikiDoc, lineNo);
                if (xPath != "")
                {
                    HtmlNode node = wikiDoc.DocumentNode.SelectSingleNode(xPath);

                    string mainCard = "main card";
                    string preliminaryCard = "preliminary card";
                    string weightClass = "weight class";
                    string fightCard = "fight card";
                    string nodeInnerText = node.InnerText.ToLower();
                    if (nodeInnerText.Contains(mainCard) || nodeInnerText.Contains(fightCard))
                    {
                        cardType = dataUtilities.GetCardType(mainCard);
                        lineNo++;
                        continue;
                    }
                    else if (nodeInnerText.Contains(preliminaryCard))
                    {
                        cardType = dataUtilities.GetCardType(preliminaryCard);
                        lineNo++;
                        continue;
                    }
                    else if (nodeInnerText.Contains(weightClass))
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
                    if (!Fight.FightInList(dataUtilities.GetAllFights(), fight))
                        dataUtilities.AddFight(fight);
                    lineNo++;
                }
                else
                    validXpath = false;
            }
            dataProcessorUI.OutputMessage(string.Format("Processed wikipedia entry for {0}", eventObj.EventName));
        }

        public void ProcessByAnalystXFights(int websiteId)
        {
            eventObj = dataUtilities.RefreshEvent(eventObj);
            Webpage webPage = dataUtilities.GetAllWebpages().FirstOrDefault(wp => wp.Event.Id == eventObj.Id && wp.Website.Id == websiteId);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(webPage.Data);

            bool validXpath = true;
            int analystStartPt = 1;
            while (validXpath)
            {
                analystStartPt++;
                string analystXPath = string.Format("//div[@class='articleBody']/table/tbody/tr[{0}]/td/strong", analystStartPt);

                HtmlNode analystNode = htmlDoc.DocumentNode.SelectSingleNode(analystXPath);
                if (analystNode == null)
                {
                    validXpath = false;
                    continue;
                }

                string analystName = analystNode.InnerText.Trim();
                Analyst analyst = FindOrAddAnalyst(analystName, websiteId);

                int fighterStartPt = 1;
                for (int i = 0; i <= eventObj.Fights.Count; i++)
                {
                    fighterStartPt++;
                    string fighterPickXPath = string.Format("//div[@class='articleBody']/table/tbody/tr[{0}]/td[{1}]", analystStartPt, fighterStartPt);
                    HtmlNode fighterPickNode = htmlDoc.DocumentNode.SelectSingleNode(fighterPickXPath);
                    if (fighterPickNode == null)
                        continue;
                    string fighterName = fighterPickNode.InnerText.Trim();
                    Fighter fighter = FindFighter(fighterName);
                    if (fighter == null)
                        continue;
                    Fight fight = dataUtilities.FindFight(fighter, eventObj);
                    Pick pick = new Pick { Analyst = analyst, Fight = fight, FighterPick = fighter };
                    dataUtilities.AddPick(pick);
                }
            }
            dataProcessorUI.OutputMessage(string.Format("Processed AnalystXFights for {0}", eventObj.EventName));
        }

        public void ProcessByFightsXAnalyst(int websiteId)
        {
            eventObj = dataUtilities.RefreshEvent(eventObj);
            Webpage webPage = dataUtilities.GetAllWebpages().FirstOrDefault(wp => wp.Event.Id == eventObj.Id && wp.Website.Id == websiteId);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(webPage.Data);

            int fightStartNo = 0;
            int i = 0;
            do
            {
                i++;
                fightStartNo++;
                string fightXPath = String.Format("//div[@class='c-entry-content']/p[contains(text(),'Staff picking')][{0}]", fightStartNo);
                HtmlNode fightNode = htmlDoc.DocumentNode.SelectSingleNode(fightXPath);
                if (fightNode == null)
                    continue;
                string fighterRegex = "(?<=Staff picking ).*?(?=:)";

                Match fighterMatch = Regex.Match(fightNode.InnerText, fighterRegex);
                string fighter1Str = fighterMatch.Value;

                string fighter2Str = fighterMatch.NextMatch().Value;
                int index = fightNode.InnerText.IndexOf(fighter2Str);
                string fighter2text = fightNode.InnerText.Substring(index);

                string fighter1Text = fightNode.InnerText.Substring(0, index);

                Fighter fighter1 = FindFighter(fighter1Str);
                if (fighter1 == null)
                    continue;

                Fight fight = dataUtilities.FindFight(fighter1, eventObj);

                string analystRegex = "(?<=(,|:) ).*?(?=(,|Staff|$))";

                Match analystMatch = Regex.Match(fighter1Text, analystRegex);

                while (analystMatch.Success)
                {
                    string analystStr = analystMatch.Value;
                    Analyst analyst = FindOrAddAnalyst(analystStr, websiteId);

                    Pick pick = new Pick() { Fight = fight, Analyst = analyst, FighterPick = fighter1 };
                    dataUtilities.AddPick(pick);
                    analystMatch = analystMatch.NextMatch();
                }

                Fighter fighter2 = FindFighter(fighter2Str);

                if (fighter2 == null)
                    continue;

                analystMatch = Regex.Match(fighter2text, analystRegex);
                while (analystMatch.Success)
                {
                    string analystStr = analystMatch.Value;
                    Analyst analyst = FindOrAddAnalyst(analystStr, websiteId);
                    Pick pick = new Pick() { Fight = fight, Analyst = analyst, FighterPick = fighter2 };
                    dataUtilities.AddPick(pick);
                    analystMatch = analystMatch.NextMatch();
                }

            } while (i <= eventObj.Fights.Count);
            dataProcessorUI.OutputMessage(string.Format("Processed FightsXAnalyst for {0}", eventObj.EventName));
        }

        private Fighter FindFighter(string name)
        {
            bool duplicateNames = dataUtilities.CheckForDuplicateNames(name, eventObj.GetAllFighters());
            if (duplicateNames) //no functionality at present to deal with duplicate surnames on a card
                return null;
            Fighter fighter = dataUtilities.FindFighter(name, eventObj);
            if (fighter != null)
                return fighter;
            if (eventObj.CancelledFighters.Contains(name))
                return null;

            dataProcessorUI.OutputMessage(string.Format("Cannot match fighter {0}. Please select from the list: ", name));
            List<Fighter> fighters = new List<Fighter>();
            eventObj.Fights.ForEach(f => fighters.AddRange(new List<Fighter>() { f.Winner, f.Loser }));
            fighters.ForEach(f => dataProcessorUI.OutputMessage(string.Format("{0}. {1}", fighters.IndexOf(f) + 1, f.FullName)));

            int number = 0;
            while (number == 0)
            {
                dataProcessorUI.OutputMessage(string.Format("Enter correct fighter number or enter w if they were withdrawn from card"));
                string fighterNo = dataProcessorUI.GetInput();
                if (fighterNo == "w")
                {
                    eventObj.CancelledFighters.Add(name);
                    return null;
                }
                else
                {
                    int.TryParse(fighterNo, out number);
                    if (number == 0 || number > fighters.Count)
                        dataProcessorUI.OutputMessage(string.Format("Incorrect input, please try again"));
                }
            }

            number--;
            fighter = fighters.ElementAt(number);
            dataUtilities.AddAltFighterName(name, fighter);
            return fighter;
        }

        private Analyst FindOrAddAnalyst(string analystName, int websiteId)
        {
            Analyst analyst = dataUtilities.FindAnalyst(analystName);
            if (analyst == null)
            {
                dataProcessorUI.OutputMessage(string.Format("Cannot find analyst: {0}\n\nPlease select from existing analysts or press n to add add as a new analyst:", analystName));
                List<Analyst> analysts = dataUtilities.GetAllAnalysts();
                analysts.ForEach(a => dataProcessorUI.OutputMessage(string.Format("{0}. {1}", analysts.IndexOf(a) + 1, a.Name)));

                bool validInput = false;
                while (!validInput)
                {
                    string analystInput = dataProcessorUI.GetInput();
                    if (analystInput == "n")
                    {
                        validInput = true;
                        analyst = dataUtilities.AddAnalyst(analystName, websiteId);
                    }
                    else
                    {
                        int.TryParse(analystInput, out int number);
                        if (number == 0 || number > analysts.Count)
                        {
                            dataProcessorUI.OutputMessage(string.Format("Incorrect input, please try again"));
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
            }
            return analyst;
        }

        ///<summary>
        ///Combines the xpath with each xpathEnding and returns the first one that finds a valid node in the document
        ///</summary>
        public static string GetCorrectXpath(string xpath, List<string> xpathEndings, HtmlDocument document, int formatNo)
        {
            string finalXpath = "";
            foreach (var xpathEnding in xpathEndings)
            {
                string xPathFormatted = string.Format(xpathEnding, formatNo);
                finalXpath = xpath + xPathFormatted;
                HtmlNode htmlNode = document.DocumentNode.SelectSingleNode(finalXpath);
                if (htmlNode != null)
                    break;
                else
                    finalXpath = "";
            }
            return finalXpath;
        }
    }
}
