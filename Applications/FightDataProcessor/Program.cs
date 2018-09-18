using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightDataProcessor.WebpageParsing;
using System;

namespace FightDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserInputCollector userInputCollector = new UserInputCollector();
            //InputParser inputParser = userInputCollector.RetrieveUserInput("Enter command: ", new CommandParser());
            //AppCommand appCommand = new AppCommand(inputParser.InputText);
            //CommandController commandController = new CommandController(appCommand);
            //commandController.HandleCommand();

            ExhibitionFinder exhibitionFinder = new ExhibitionFinder(new FightPicksContext());
            foreach (Exhibition exhibition in exhibitionFinder.FindAllExhibitions())
                new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Console.WriteLine("Finished processing data");
            Console.ReadLine();
        }
    }
}
