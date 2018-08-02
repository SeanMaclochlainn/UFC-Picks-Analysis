using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class AnalystParser
    {
        private XDocument htmlDocument;

        public AnalystParser(XDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseAnalyst(int currentRow)
        {
            XElement analystElement = htmlDocument.XPathSelectElement(XpathGenerator.GetAnalystXpath(currentRow));
            if (analystElement != null)
                return analystElement.Value;
            else
                return "";
        }
    }
}
