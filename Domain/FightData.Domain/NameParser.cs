using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
            names.RemoveAt(0);
            names.RemoveAt(names.Count - 1);
            foreach (string name in names)
                middleNames += name;
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
            RemoveAccents();
        }

        private void RemoveAccents()
        {
            fullName = fullName.Normalize(NormalizationForm.FormD);
            char[] chars = fullName.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            fullName = new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
