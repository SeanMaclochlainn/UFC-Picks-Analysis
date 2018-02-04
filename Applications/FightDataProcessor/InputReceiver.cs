using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class InputReceiver
    {
        public virtual string GetInput()
        {
            return Console.ReadLine();
        }
    }
}
