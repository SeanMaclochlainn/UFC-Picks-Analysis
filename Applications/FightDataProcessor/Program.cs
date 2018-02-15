using FightData.DataLayer;
using FightData.Models.DataModels;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FightDataProcessor
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            DataUtilities dataUtilities = new DataUtilities();

        //Configuration = new ConfigurationBuilder()
        //.SetBasePath(Directory.GetCurrentDirectory())
        //.AddJsonFile("appsettings.json", false, true)
        //.Build();
        //string connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        Console.WriteLine("Enter command:");
            bool validInput = false;
            while (!validInput)
            {
                validInput = true;
                string input = Console.ReadLine();
                input = input.ToLower();
                EventDataHandler eventDataHandler = new EventDataHandler();

                if (input == "collect")
                {
                    eventDataHandler.CollectEventData();
                }
                else if (input == "process" || input == "process -clearexisting")
                {
                    if (input == "process -clearexisting")
                    {
                        eventDataHandler.DeleteAllPicks();
                    }

                    List<Event> events = dataUtilities.GetAllEvents();
                    foreach (var eventObj in events)
                    {
                        WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj, dataUtilities);
                        webpageProcessor.ProcessWebpages();
                    }
                }
                else
                {
                    validInput = false;
                    Console.WriteLine("Invalid input. Please re-enter");
                }
            }

            Console.WriteLine("Finished processing data");
            Console.ReadLine();
        }        
    }
}
