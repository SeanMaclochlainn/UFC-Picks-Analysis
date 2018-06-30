using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor
{
    public class CommandParser : InputParser
    {
        private string command;
        private List<string> arguments;

        //public CommandParser()
        //{

        //}

        //public CommandParser(string commandText)
        //{
        //    command = ParseCommand();
        //    arguments = ParseArguments();
        //    AppCommand = new AppCommand(command, arguments);
        //}

        public AppCommand AppCommand { get; private set; }        

        public override void ParseInput(string inputText)
        {
            base.ParseInput(inputText);
            command = AppCommand.ParseCommand(inputText);
            arguments = AppCommand.ParseArguments(inputText);
        }

        public override bool IsValid()
        {
            if (!base.IsValid())
                return false;
            
            if (!Commands.AllCommands.Contains(AppCommand.ParseCommand(InputText)))
                return false;

            if (arguments.Any(a => Arguments.AllArguments.Contains(a) == false))
                return false;

            return true;
        }

        //private string ParseCommand()
        //{
        //    return inputText.Split(' ')[0];
        //}

        //private List<string> ParseArguments()
        //{
        //    List<string> arguments = inputText.Split(' ').ToList();
        //    arguments.RemoveAt(0);
        //    arguments = arguments.Select(a => a.Replace(Arguments.ArgumentPrefix, "")).ToList();
        //    return arguments;
        //}
    }
}
