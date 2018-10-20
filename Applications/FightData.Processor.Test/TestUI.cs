using System.Collections.Generic;
using System.Linq;
using FightDataProcessor;

namespace FightDataProcessorTest
{
    public class TestUI : AppUi
    {
        private int index;
        private List<string> inputsList;

        public TestUI(List<string> inputs)
        {
            this.inputsList = inputs;
        }

        public override string GetNextInput()
        {
            string input = inputsList.ElementAt(index);
            index++;
            return input;
        }
    }

    //class TestUI : Ui
    //{
    //    private int index;
    //    private List<string> inputsList;
    //    private IConfigurationRoot appConfiguration;

    //    public TestUI(List<string> inputs, IConfigurationRoot appConfig)
    //    {
    //        this.inputsList = inputs;
    //        this.appConfiguration = appConfig;
    //    }

    //    public override string GetInput()
    //    {
    //        string input = inputsList.ElementAt(index);
    //        index++;
    //        OutputMessage(input);
    //        return input;
    //    }

    //    public override void OutputMessage(string message)
    //    {
    //        string logsFolder = appConfiguration["LogFileFolder"];
    //        logsFolder += "\\UnitTestOutput";
    //        Directory.CreateDirectory(logsFolder);
    //        File.AppendAllText($"{logsFolder}\\console.txt", $"{message}\n");
    //    }
    //}
}
