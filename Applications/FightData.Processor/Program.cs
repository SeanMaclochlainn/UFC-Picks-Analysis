using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightDataProcessor.WebpageParsing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FightDataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptions<FightPicksContext> dbContextOptions = new DbContextOptionsBuilder<FightPicksContext>()
               .UseSqlServer(configuration.GetConnectionString("FightPicks"))
               .Options;

            ExhibitionFinder exhibitionFinder = new ExhibitionFinder(new FightPicksContext(dbContextOptions));
            foreach (Exhibition exhibition in exhibitionFinder.FindAllExhibitions())
                new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Console.WriteLine("Finished processing data");
            Console.ReadLine();
        }
    }
}
