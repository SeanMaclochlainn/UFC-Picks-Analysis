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
            List<string> names = SplitName();
            if (names.Count > 1)
                return names.First();
            else
                return "";
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
            if (names.Count > 2)
            {
                for (int i = 1; i < names.Count - 1; i++)
                    middleNames += names[i] + " ";
            }
            return middleNames.Trim();
        }

        public string GetFullName()
        {
            string middleName = GetMiddleNames();
            string firstname = GetFirstName();
            if(string.IsNullOrEmpty(middleName) && string.IsNullOrEmpty(firstname))
                return $"{GetLastName()}";
            else if (string.IsNullOrEmpty(middleName))
                return $"{GetFirstName()} {GetLastName()}";
            else
                return $"{GetFirstName()} {middleName} {GetLastName()}";

        }

        private List<string> SplitName()
        {
            return nameText.Split(' ').ToList();
        }

        private void Sanitize()
        {
            nameText = nameText.Replace("(c)", "");
            nameText = nameText.Replace(".", "");
            nameText = nameText.Replace("Jr", "");
            nameText = nameText.Trim();
            nameText = nameText.ToLower();
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
