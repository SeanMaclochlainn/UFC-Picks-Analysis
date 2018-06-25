using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class YesNoQuestionParser : InputParser
    {
        public override void ParseInput(string inputText)
        {
            InputText = inputText.ToLower();
        }

        public override bool IsValid()
        {
            if (!base.IsValid())
                return false;
            if (InputText != "y" && InputText != "n")
                return false;
            else
                return true;
        }
    }
}
