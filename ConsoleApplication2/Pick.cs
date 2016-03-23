using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Pick
    {
        private string analystName;
        private string pick;
        private Fight fight;

        public Pick(string analystName, string pick, Fight fight)
        {
            this.analystName = analystName;
            this.pick = pick;
            this.fight = fight;
        }

        public string getAnalystName()
        {
            return analystName;
        }

        public string getPick()
        {
            return pick;
        }

        public Fight getFight()
        {
            return fight;
        }
    }
}
