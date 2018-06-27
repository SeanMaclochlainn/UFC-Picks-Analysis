using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain
{
    public class NameParser
    {
        private string fullName;
        private List<string> names;

        public NameParser(string fullName)
        {
            this.fullName = fullName;
            SplitName();
            FirstName = names.First();
            LastName = names.Last();
            GetMiddleNames();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleNames { get; private set; }

        private void SplitName()
        {
            names = fullName.Split(' ').ToList();
        }

        private void GetMiddleNames()
        {
            names.Remove(FirstName);
            names.Remove(LastName);
            foreach (string name in names)
                MiddleNames += name;
        }
    }
}
