using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FightData.Domain
{
    public class NameParser
    {
        private string nameText;

        public NameParser(string nameText)
        {
            this.nameText = nameText;
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

        public string GetFullName()
        {
            string middleName = GetMiddleNames();
            if(string.IsNullOrEmpty(middleName))
                return $"{GetFirstName()} {GetLastName()}";
            else
                return $"{GetFirstName()} {GetMiddleNames()} {GetLastName()}";

        }

        private List<string> SplitName()
        {
            return nameText.Split(' ').ToList();
        }

        private void Sanitize()
        {
            nameText = nameText.Replace("(c)", "");
            nameText = nameText.Trim();
            RemoveAccents();
        }

        private void RemoveAccents()
        {
            nameText = nameText.Normalize(NormalizationForm.FormD);
            char[] chars = nameText.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            nameText = new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
