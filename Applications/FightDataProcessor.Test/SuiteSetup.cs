using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
