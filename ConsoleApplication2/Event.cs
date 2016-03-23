using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace ConsoleApplication2
{
    class Event : EventsProcessor
    {
        protected string fightName;
        protected string bloodyElbowFilePath;
        protected string mmajunkieFilepath;
        protected List<Fight> fights;
        protected List<Pick> Picks;
        protected string bettingFilePath;

        public Event(string wikiFilePath)
        {
            //get bloodyElbow file path
            bloodyElbowFilePath = wikiFilePath.Replace("wiki", "be staff picks");

            //get mma junkie file path
            mmajunkieFilepath = wikiFilePath.Replace("wiki", "mj staff picks");

            //get betting file path
            bettingFilePath = wikiFilePath.Replace("wiki", "odds");

            //get the fight name
            var noFolders = wikiFilePath.Split('\\').Count();
            string fileName = wikiFilePath.Split('\\')[noFolders - 1];
            fightName = fileName.Replace(" wiki.txt", "");

            //get fights from file
            List<string> wikiFileLines = File.ReadAllLines(@wikiFilePath).ToList();
            wikiFileLines.RemoveRange(0, 2);
            fights = new List<Fight>();
            foreach (var line in wikiFileLines)
            {
                string fighterA = line.Split('\t')[1].Split(' ')[1];
                string fighterB = line.Split('\t')[3].Split(' ')[1];
                string winner = line.Split('\t')[1].Split(' ')[1];
                fights.Add(new Fight(fighterA, fighterB, winner)); //need to add handling for incorrect names
            }

            //get bloody elbow picks and set fight odds
            Picks = new List<Pick>();
            List<string> bettingFileLines = File.ReadAllLines(@bettingFilePath).ToList();
            foreach (var fight in fights)
            {
                foreach(var analyst in this.analysts)
                {
                    string pick = getAnalystsPick(fight, analyst);
                    Pick pickObj = null;
                    if(pick != "")
                    {
                        pickObj = new Pick(analyst, pick, fight);
                    }
                    else
                    {
                        pickObj = new Pick(analyst, "", fight);
                    }
                    Picks.Add(pickObj);

                }
                
                fight.setOdds(bettingFileLines);                
            }
        }

        public string getAnalystsPick(Fight fight, string analyst)
        {
            List<string> beFileLines = File.ReadAllLines(@bloodyElbowFilePath).ToList();
            List<string> mmaJunkieLines = File.ReadAllLines(@mmajunkieFilepath).ToList();

            //if analyst is in bloody elbow files, get picks from there, otherwise look for analyst in bloody elbow file
            int analystIndex = mmaJunkieLines.IndexOf(analyst);
            if (analystIndex == -1)
                analystIndex = mmaJunkieLines.IndexOf(analyst + " ");
            if (analystIndex != -1) 
            {
                //get analyst's section from file
                mmaJunkieLines.RemoveRange(0, analystIndex+2);
                
                if(mmaJunkieLines.Any(i=>i.Contains("@")))
                {
                    var nextAnalystsTwitterName = mmaJunkieLines.Where(i => i.Contains("@")).ElementAt(0);
                    mmaJunkieLines.RemoveRange(mmaJunkieLines.IndexOf(nextAnalystsTwitterName) - 1, mmaJunkieLines.Count - mmaJunkieLines.IndexOf(nextAnalystsTwitterName));
                }



                //remove \t's from file
                for (int i=0;i<mmaJunkieLines.Count;i++)
                {
                    if (mmaJunkieLines[i].IndexOf("\t") != -1)
                    {
                        int x = mmaJunkieLines[i].Length - mmaJunkieLines[i].IndexOf("\t");
                        mmaJunkieLines[i] = mmaJunkieLines[i].Remove(mmaJunkieLines[i].IndexOf("\t"),x);
                    }
                }
                //mmajunkieLines now contains that analysts section, so it can now be checked to see if it contains any fighters
                if(mmaJunkieLines.Contains(fight.FighterA()))
                {
                    return fight.FighterA();
                }
                else if(mmaJunkieLines.Contains(fight.FighterB()))
                {
                    return fight.FighterB();
                }
            }
            else
            {
                foreach (var line in beFileLines.Where(i => i.Split(' ').Contains(fight.FighterA() + ":") || i.Split(' ').Contains(fight.FighterB() + ":")))
                {
                    if (line.Split(' ').Contains(analyst) || line.Split(' ').Contains(analyst + ","))
                    {
                        string pick = line.Split(' ')[2];
                        return pick.Remove(pick.Length - 1);
                    }
                }
            }

            //if a pick isn't found for this analyst, return ""
            return "";
        }

        public double getAnalystsPostEventBalance(string analyst, double analystPreEventBalance)
        {
            double analystPostEventBalance = 0;

            //get number of fights that analyst has picked
            int fightsWithPicksCount = 0;
            foreach(var fight in fights)
            {
                if(getAnalystsPick(fight,analyst).Equals("")== false)
                {
                    fightsWithPicksCount++;
                }
            }

            //return the original balance if the analyst hasn't picked any fights in the event
            if (fightsWithPicksCount == 0)
                return analystPreEventBalance;

            //compute how much to spend on each fight
            //double stake = (analystPreEventBalance*.5) / fightsWithPicksCount;
            double stake = 1;

            //subtract all stakes from balance (i.e. place bets)
            analystPostEventBalance = analystPreEventBalance - (stake * fightsWithPicksCount);

            //compute analysts total
            foreach(var fight in fights)
            {
                string pick = getAnalystsPick(fight, analyst);
                if(pick.Equals(fight.Winner()) && pick.Equals("")==false)
                {
                    if (fight.FighterA().Equals(fight.Winner()))
                        analystPostEventBalance += (fight.FighterAOdds())*stake;
                    else
                        analystPostEventBalance += (fight.FighterBOdds())*stake;
                }
                
            }
            return analystPostEventBalance;
        }

        public int getNoOfFights()
        {
            return fights.Count;
        }

        public List<Fight> getfights()
        {
            return fights;
        }

        public void writeToExcel(Application application, Workbook workBook, Worksheet workSheet, int startingRow)
        {

            //enter event title
            workSheet.Cells[startingRow + 1, 1] = fightName;
            
            //enter headers
            workSheet.Cells[startingRow + 1, 2] = "Fighter A";
            workSheet.Cells[startingRow + 1, 3] = "Betting Odds (Fighter A)";
            workSheet.Cells[startingRow + 1, 4] = "Fighter B";
            workSheet.Cells[startingRow + 1, 5] = "Betting Odds (Fighter B)";
            workSheet.Cells[startingRow+1, 6] = "Winner";
            for(int i=0;i<analysts.Count;i++)
            {
                workSheet.Cells[startingRow+1, i + 7] = analysts[i];
            }

            //enter fighters
            int fightCounter = 2;
            foreach(var fight in fights)
            {
                workSheet.Cells[startingRow + fightCounter, 2] = fight.FighterA();
                workSheet.Cells[startingRow + fightCounter, 3] = fight.FighterAOdds();
                workSheet.Cells[startingRow + fightCounter, 4] = fight.FighterB();
                workSheet.Cells[startingRow + fightCounter, 5] = fight.FighterBOdds();
                workSheet.Cells[startingRow + fightCounter, 6] = fight.Winner();

                //enter each analysts picks
                int analystCounter = 0;
                foreach(var analyst in analysts)
                {
                    Pick pick = Picks.Single(i => i.getFight() == fight && i.getAnalystName() == analyst);
                    workSheet.Cells[startingRow+fightCounter, analystCounter + 7] = pick.getPick();
                    if (pick.getPick() == fight.Winner())
                    {
                        workSheet.Cells[startingRow + fightCounter, analystCounter + 7].Interior.Color = XlRgbColor.rgbGreen;
                    }
                    else if (pick.getPick() == fight.Loser())
                    {
                        workSheet.Cells[startingRow + fightCounter, analystCounter + 7].Interior.Color = XlRgbColor.rgbRed;
                    }
                    analystCounter++;
                }
                fightCounter++;
            }

        }

    }
}
