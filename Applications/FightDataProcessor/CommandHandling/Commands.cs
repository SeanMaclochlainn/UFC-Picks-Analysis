using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public static class Commands
    {
        public readonly static string Collect = "collect";
        public readonly static string Process = "process";

        public static List<string> AllCommands = new List<string>() { Collect, Process };
    }
}
