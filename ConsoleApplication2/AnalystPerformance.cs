using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class AnalystPerformance
    {
        private string analystName;
        private int correctPicks;
        private int incorrectPicks;
        //private double totalNetProfit;
        private double balance;

        public AnalystPerformance(string analystName)
        {
            this.analystName = analystName;
            balance = 50;
        }

        public string getAnalystName()
        {
            return analystName;
        }

        public double getBalance()
        {
            return Math.Round(balance, 2);
        }

        public void addWin()
        {
            correctPicks++;
        }
        
        public void addLoss()
        {
            incorrectPicks++;
        }

        public double getPercentageCorrectPicks()
        {
            if(correctPicks>0)
            {
                double result = correctPicks / (double)(correctPicks + incorrectPicks);
                return result;
            }
            else
            {
                return 0;
            }
        }

        public void setBalance(double netProfit)
        {
            balance = netProfit;
        }

        //public double getTotalNetProfit()
        //{
        //    return totalNetProfit;
        //}
    }
}
