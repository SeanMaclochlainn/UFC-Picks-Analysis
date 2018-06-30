using System.IO;
using Microsoft.Extensions.Configuration;

namespace FightDataProcessorTest
{
    class SuiteSetup
    {
        public IConfigurationRoot Configuration;

        public SuiteSetup()
        {
            string baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile("AppSettings.json")
                .Build();
        }
    }
}
