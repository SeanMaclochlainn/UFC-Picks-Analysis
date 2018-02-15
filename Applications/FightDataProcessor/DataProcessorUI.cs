using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class DataProcessorUI
    {
        public virtual string GetInput()
        {
            return Console.ReadLine();
        }

        public virtual void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
