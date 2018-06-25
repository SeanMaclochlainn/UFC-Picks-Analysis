using FightDataProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessorTest
{
    [TestClass]
    public class TestCommands
    {
        private CommandParser commandParser;

        public TestCommands()
        {
            commandParser = new CommandParser();
        }
        
        [TestMethod]
        public void TestInvalidInput()
        {
            string rawInput = "process clearexistingdata";
            
            commandParser.ParseInput(rawInput);

            Assert.IsFalse(commandParser.IsValid());
        }

        [TestMethod]
        public void TestParsingArguments()
        {
            string rawInput = "process -clearexisting";
            
            commandParser.ParseInput(rawInput);
            AppCommand appCommand = new AppCommand(commandParser.InputText);
            
            Assert.IsTrue(appCommand.Arguments[0] == Arguments.ClearExisting);
        }

        [TestMethod]
        public void TestParsingCommand()
        {
            string rawInput = "process -clearexisting";
            
            commandParser.ParseInput(rawInput);
            AppCommand appCommand = new AppCommand(commandParser.InputText);

            Assert.IsTrue(appCommand.Command == Commands.Process);
        }

        [TestMethod]
        public void TestEnteringCommand()
        {
            string command = string.Format("{0} -{1}", Commands.Process, Arguments.ClearExisting);
            
            commandParser.ParseInput(command);
            AppCommand appCommand = new AppCommand(commandParser.InputText);

            Assert.IsTrue(appCommand.Command == Commands.Process);
        }


    }
}
