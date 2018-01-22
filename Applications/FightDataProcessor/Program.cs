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
            FightPicksContext db = new FightPicksContext();

            DataUtilities dataUtilities = new DataUtilities();

            //Configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", false, true)
            //.Build();
            //string connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            Console.WriteLine("1: Collect Data\n2: Process Events");
            var input = Console.ReadLine();
            if (input == "1")
            {
                while (true)
                {
                    Console.WriteLine("Enter the event name or press e to select existing event");
                    string eventSelection = Console.ReadLine();
                    int eventId = 0;
                    Event eventObj = null;
                    if (eventSelection == "e")
                    {
                        List<Event> eventList = dataUtilities.GetAllEvents();
                        Console.WriteLine("Select event ID:");
                        foreach (var existingEvent in eventList)
                        {
                            Console.WriteLine("ID: {0} Name: {1}", existingEvent.Id, existingEvent.EventName);
                        }
                        eventId = int.Parse(Console.ReadLine());
                        eventObj = eventList.First(e => e.Id == eventId);
                    }
                    else
                    {
                        eventObj = new Event() { EventName = eventSelection };
                        dataUtilities.AddEvent(eventObj);
                    }

                    List<Website> websites = dataUtilities.GetAllWebsites();
                    HttpClient client = new HttpClient();
                    foreach (var website in websites)
                    {
                        Webpage webpage = new Webpage();
                       Console.WriteLine("Enter {0} url (Enter to skip website)", website.WebsiteName.ToString());
                        string websiteUrl = Console.ReadLine();
                        if (!string.IsNullOrEmpty(websiteUrl))
                        {
                            webpage.Url = websiteUrl;
                            webpage.Event = eventObj;
                            webpage.Website = website;
                            Task<HttpResponseMessage> result = client.GetAsync(websiteUrl);
                            while (!result.IsCompleted)
                            {

                            }
                            HttpResponseMessage result2 = result.Result;
                            HttpContent content = result2.Content;
                            Task<string> contentstr = content.ReadAsStringAsync();
                            while (!contentstr.IsCompleted)
                            {

                            }
                            webpage.Data = contentstr.Result;
                            //webpage.Data = client.DownloadString(websiteUrl);
                            dataUtilities.AddWebpage(webpage);
                        }
                    }
                }
            }
            else if (input == "2")
            {
                var events = dataUtilities.GetAllEvents();
                foreach (var eventObj in events)
                {
                    WebpageProcessor webpageProcessor = new WebpageProcessor(eventObj);
                    webpageProcessor.ProcessWebpages();
                }
            }            
        }        
    }
}
