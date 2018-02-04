using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FightDataProcessor;

namespace FightDataProcessorTest
{
    public class TestInputReceiver : InputReceiver
    {
        private int index;
        private List<string> inputsList;

        public TestInputReceiver(List<string> inputsList)
        {
            this.inputsList = inputsList;
        }

        public override string GetInput()
        {
            string input = inputsList.ElementAt(index);
            index++;
            return input;
        }
    }
}
