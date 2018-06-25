using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class UserInputCollector
    {
        private AppUi ui;

        public UserInputCollector() : this(new AppUi()) { }

        public UserInputCollector(AppUi ui)
        {
            this.ui = ui;
        }

        public InputParser RetrieveUserInput(string prompt)
        {
            return RetrieveUserInput(prompt, new InputParser());
        }

        public InputParser RetrieveUserInput(string prompt, InputParser inputParser)
        {
            ui.OutputMessage(prompt);
            int counter = 0;
            bool validInput = false;
            while (!validInput)
            {
                if (counter > 0)
                    ui.OutputMessage(inputParser.InvalidInputMessage);
                inputParser.ParseInput(ui.GetNextInput());
                validInput = inputParser.IsValid();
                counter++;
            }
            return inputParser;
        }
    }
}
