using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class FightersParser
    {
        private static int maxNoOfFights = 10;
        private XDocument htmlDocument;
        private int currentRow;

        public FightersParser(XDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public List<string> ParseFighters(int rowNo)
        {
            currentRow = rowNo;
            List<string> fighters = new List<string>();
            foreach (XElement fighterElement in GetCurrentRowFighterElements())
                fighters.Add(DataSanitizer.GetElementValue(fighterElement));
            return fighters;
        }

        private List<XElement> GetCurrentRowFighterElements()
        {
            List<XElement> fighterElements = new List<XElement>();
            for (int currentColumnNo = 1; currentColumnNo <= maxNoOfFights; currentColumnNo++)
            {
                XElement parsedFighter = ParseFighter(currentColumnNo);
                if (parsedFighter != null)
                    fighterElements.Add(parsedFighter);
            }
            return fighterElements;
        }

        private XElement ParseFighter(int columnNo)
        {
            return htmlDocument.XPathSelectElement(XpathGenerator.GetFighterXpath(currentRow, columnNo));
        }

    }
}
