using FightData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor
{
    public class UfcEventCollectingUi
    {
        private AppUi ui;
        private UserInputCollector userInputCollector;

        public UfcEventCollectingUi() : this(new AppUi()) { }

        public UfcEventCollectingUi(AppUi ui)
        {
            this.ui = ui;
            this.userInputCollector = new UserInputCollector(ui);
        }

        public bool IsNewEvent()
        {
            InputParser inputParser = userInputCollector.RetrieveUserInput("Is this a new event? (y/n)", new YesNoQuestionParser());
            if (inputParser.InputText == "y")
                return true;
            else
                return false;
        }

        public string GetEventName()
        {
            InputParser inputParser = userInputCollector.RetrieveUserInput("Enter event name");
            return inputParser.InputText;
        }

        public UfcEvent SelectEvent(List<UfcEvent> ufcEvents)
        {
            string messageText = "Select event:\r\n";
            for(int i=0;i<ufcEvents.Count;i++)
                messageText += string.Format("{0} Name: {1}\r\n", i + 1, ufcEvents[i].EventName);
            InputParser inputParser = userInputCollector.RetrieveUserInput(messageText, new ListSelectionParser(ufcEvents));
            return ufcEvents.ElementAt(int.Parse(inputParser.InputText) - 1);
        }

        public string GetWebsiteUrl(Website website)
        {
            string userPrompt = String.Format("Enter {0} url (Enter to skip website)", website.WebsiteName.ToString());
            InputParser inputParser = userInputCollector.RetrieveUserInput(userPrompt, new WebsiteUrlParser());
            return inputParser.InputText;
        }
    }
}
