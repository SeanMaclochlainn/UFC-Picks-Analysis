using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FightDataProcessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FightDataProcessorTest
{
    public class TestDataProcessorUI : DataProcessorUI
    {
        private int index;
        private List<string> inputsList;
        private IConfigurationRoot appConfiguration;

        public TestDataProcessorUI(List<string> inputsList, IConfigurationRoot appConfig)
        {
            this.inputsList = inputsList;
            this.appConfiguration = appConfig;
        }

        public override string GetInput()
        {
            string input = inputsList.ElementAt(index);
            index++;
            OutputMessage(input);
            return input;
        }

        public override void OutputMessage(string message)
        {
            string logsFolder = appConfiguration["LogFileFolder"];
            logsFolder += "\\UnitTestOutput";
            Directory.CreateDirectory(logsFolder);
            File.AppendAllText($"{logsFolder}\\console.txt", $"{message}\n");
        }
    }
}
