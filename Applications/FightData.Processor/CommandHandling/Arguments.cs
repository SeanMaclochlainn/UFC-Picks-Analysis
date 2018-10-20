using System.Collections.Generic;

namespace FightDataProcessor
{
    public class Arguments
    {
        public readonly static string ArgumentPrefix = "-";
        public readonly static string ClearExisting = "clearexisting";
        public readonly static List<string> AllArguments = new List<string>() { ClearExisting };
    }
}
