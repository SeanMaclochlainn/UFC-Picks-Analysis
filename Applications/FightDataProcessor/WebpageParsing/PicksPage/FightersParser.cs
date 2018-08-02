using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class FightersParser
    {
        private XDocument htmlDocument;
        private static int maxNoOfFights = 10;
        private int currentRow;
        private int columnNo;

        public FightersParser(XDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public List<string> ParseFighters(int rowNo)
        {
            this.currentRow = rowNo;
            List<string> fighters = new List<string>();
            foreach (XElement fighterElement in GetFighterElements())
                fighters.Add(fighterElement.Value.Trim());
            return fighters;
        }

        private List<XElement> GetFighterElements()
        {
            List<XElement> fighterElements = new List<XElement>();
            for (int i = 1; i <= maxNoOfFights; i++)
            {
                columnNo = i;
                XElement parsedFighter = ParseFighter();
                if (parsedFighter != null)
                    fighterElements.Add(parsedFighter);
            }
            return fighterElements;
        }

        private XElement ParseFighter()
        {
            return htmlDocument.XPathSelectElement(XpathGenerator.GetFighterXpath(currentRow, columnNo));
        }
    }
}
