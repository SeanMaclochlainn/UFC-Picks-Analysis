using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightDataProcessor
{
    public class ListSelectionParser : InputParser
    {
        private IEnumerable<object> itemList;

        public ListSelectionParser(IEnumerable<object> itemList)
        {
            this.itemList = itemList;
        }

        public int itemIndex { get; private set; }

        public override string InvalidInputMessage
        {
            get
            {
                return "Invalid input. Please select a valid number from the list";
            }
        }

        public override void ParseInput(string inputText)
        {
            base.ParseInput(inputText);
            itemIndex = int.Parse(InputText) - 1;
        }

        public override bool IsValid()
        {
            if (!base.IsValid())
                return false;
            int input;
            if (!int.TryParse(InputText, out input))
                return false;
            if (itemList.Count() < input)
                return false;
            return true;
        }
    }
}
