using FightData.Domain.Entities;
using System;

namespace FightDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInputCollector userInputCollector = new UserInputCollector();
            InputParser inputParser = userInputCollector.RetrieveUserInput("Enter command: ", new CommandParser());
            AppCommand appCommand = new AppCommand(inputParser.InputText);
            CommandController commandController = new CommandController(appCommand);
            commandController.HandleCommand();

            Console.WriteLine("Finished processing data");
            Console.ReadLine();
        }
    }
}
