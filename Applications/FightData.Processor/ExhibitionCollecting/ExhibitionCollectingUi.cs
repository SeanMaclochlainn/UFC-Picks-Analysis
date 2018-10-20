using FightData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor
{
    public class ExhibitionCollectingUi
    {
        private AppUi ui;
        private UserInputCollector userInputCollector;

        public ExhibitionCollectingUi() : this(new AppUi()) { }

        public ExhibitionCollectingUi(AppUi ui)
        {
            this.ui = ui;
            this.userInputCollector = new UserInputCollector(ui);
        }

        public bool IsNewExhibition()
        {
            InputParser inputParser = userInputCollector.RetrieveUserInput("Is this a new exhibition? (y/n)", new YesNoQuestionParser());
            if (inputParser.InputText == "y")
                return true;
            else
                return false;
        }

        public string GetExhibitionName()
        {
            InputParser inputParser = userInputCollector.RetrieveUserInput("Enter exhibition name");
            return inputParser.InputText;
        }

        public Exhibition SelectExhibition(List<Exhibition> exhibitions)
        {
            string messageText = "Select exhibition:\r\n";
            for(int i=0;i<exhibitions.Count;i++)
                messageText += string.Format("{0} Name: {1}\r\n", i + 1, exhibitions[i].Name);
            InputParser inputParser = userInputCollector.RetrieveUserInput(messageText, new ListSelectionParser(exhibitions));
            return exhibitions.ElementAt(int.Parse(inputParser.InputText) - 1);
        }

        public string GetWebsiteUrl(Website website)
        {
            string userPrompt = String.Format("Enter {0} url (Enter to skip website)", website.WebsiteName.ToString());
            InputParser inputParser = userInputCollector.RetrieveUserInput(userPrompt, new WebsiteUrlParser());
            return inputParser.InputText;
        }
    }
}
