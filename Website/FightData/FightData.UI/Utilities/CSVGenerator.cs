using System.Collections.Generic;

namespace FightData.UI.Utilities
{
    public class CsvGenerator
    {
        private string content;

        public CsvGenerator()
        {
            content = "";
        }

        public void AddRow(List<string> entries)
        {
            foreach (string entry in entries)
                content += entry + ",";
            content = content.Remove(content.Length - 1);
            content += "\r\n";
        }

        public string GetContent()
        {
            return content;
        }
    }
}
