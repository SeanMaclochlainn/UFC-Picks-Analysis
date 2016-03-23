using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Fight
    {
        private string fighterA;
        private string fighterB;
        private string winner;
        private double fighterAOdds;
        private double fighterBOdds;

        public Fight(string fighterA, string fighterB, string winner)
        {
            this.fighterA = fighterA;
            this.fighterB = fighterB;
            this.winner = winner;
        }

        public string FighterA()
        {
            return fighterA;
        }

        public string FighterB()
        {
            return fighterB;
        }

        public string Winner()
        {
            return winner;
        }

        public string Loser()
        {
            if(winner.Equals(fighterA))
            {
                return fighterB;
            }
            else
            {
                return fighterA;
            }
        }

        public double FighterAOdds()
        {
            return fighterAOdds;
        }

        public double FighterBOdds()
        {
            return fighterBOdds;
        }

        public void setOdds(List<string> bettingFileLines)
        {
            bettingFileLines.RemoveAt(0);
            int counter = 0;
            while (fighterAOdds == 0 && fighterBOdds == 0 && counter<50)
            {
                foreach (var line in bettingFileLines)
                {
                    var lineWordsArray = line.Split('\t');
                    if (fighterA == lineWordsArray[0].Split(' ')[1])
                    {
                        retrieveAndSetOdds(lineWordsArray, fighterA);
                    }
                    else if (fighterB == lineWordsArray[0].Split(' ')[1])
                    {
                        retrieveAndSetOdds(lineWordsArray, fighterB);
                    }
                    counter++;
                }
            }
        }

        private void retrieveAndSetOdds(string[] lineWordsArray, string fighter)
        {
            string moneyLineOdds = lineWordsArray[5];
            if (moneyLineOdds.ToCharArray()[0] == '+')
            {
                moneyLineOdds.Remove(0, 1);
                int moneyLineInt = int.Parse(moneyLineOdds);
                if (fighter == fighterA)
                    fighterAOdds = Math.Round(((double)moneyLineInt / 100) + 1, 2);
                else
                    fighterBOdds = Math.Round(((double)moneyLineInt / 100) + 1, 2);
            }
            else
            {
                moneyLineOdds = moneyLineOdds.Remove(0, 1);
                int moneyLineInt = int.Parse(moneyLineOdds);
                if (fighter == fighterB)
                    fighterBOdds = Math.Round((100 / (double)moneyLineInt) + 1, 2);
                else
                    fighterAOdds = Math.Round((100 / (double)moneyLineInt) + 1, 2);
            }
        }
    }
}
