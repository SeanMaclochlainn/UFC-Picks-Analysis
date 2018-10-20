using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain
{
    public class NameParser
    {
        private string fullName;

        public NameParser(string fullName)
        {
            this.fullName = fullName;
        }

        public string GetFirstName()
        {
            Sanitize();
            return SplitName().First();
        }

        public string GetLastName()
        {
            Sanitize();
            return SplitName().Last();
        }

        public string GetMiddleNames()
        {
            Sanitize();
            List<string> names = SplitName();
            string middleNames = "";
            for (int i = 1; i < names.Count; i++)
                middleNames += names[i];
            return middleNames;
        }

        private List<string> SplitName()
        {
            return fullName.Split(' ').ToList();
        }

        private void Sanitize()
        {
            fullName = fullName.Replace("(c)", "");
            fullName = fullName.Trim();
        }
    }
}
