using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightDataProcessor
{
    public class AppCommand
    {
        public AppCommand(string commandText)
        {
            this.Command = ParseCommand(commandText);
            this.Arguments = ParseArguments(commandText);
        }

        public AppCommand(string command, List<string> arguments)
        {
            this.Command = command;
            this.Arguments = arguments;
        }

        public static string ParseCommand(string commandText)
        {
            return commandText.Split(' ')[0];
        }

        public static List<string> ParseArguments(string commandText)
        {
            List<string> arguments = commandText.Split(' ').ToList();
            arguments.RemoveAt(0);
            arguments = arguments.Select(a => a.Replace(FightDataProcessor.Arguments.ArgumentPrefix, "")).ToList();
            return arguments;
        }

        public string Command { get; set; }
        public List<string> Arguments { get; set; }

    }
}
