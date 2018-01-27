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

        Console.WriteLine("Choose from the following options: \n\n1: Collect Data\n2: Process Events");
            string input = Console.ReadLine();
            if (input == "1")
            {
                EventDataCollector eventDataCollector = new EventDataCollector();
                eventDataCollector.CollectEventData();
            }
            else if (input == "2")
            {
                List<Event> events = dataUtilities.GetAllEvents();
                foreach (var eventObj in events)
                {
                    WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj);
                    webpageProcessor.ProcessWebpages();
                }
            }
            Console.WriteLine("Finished processing data");
            Console.ReadLine();
        }        
    }
}
