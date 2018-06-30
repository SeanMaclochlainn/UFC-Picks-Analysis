using System;

namespace FightDataProcessor
{
    public class AppUi
    {
        public virtual string GetNextInput()
        {
            return Console.ReadLine();
        }

        public virtual void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
