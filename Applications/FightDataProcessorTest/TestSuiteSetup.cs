using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightDataProcessorTest
{
    public class TestSuiteSetup
    {
        public IConfigurationRoot Configuration;

        public TestSuiteSetup()
        {
            string baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile("AppSettings.json")
                .Build();
        }
    }
}
