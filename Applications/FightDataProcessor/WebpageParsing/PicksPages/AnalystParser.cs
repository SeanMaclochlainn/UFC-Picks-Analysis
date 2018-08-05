using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class AnalystParser
    {
        private XDocument htmlDocument;

        public AnalystParser(XDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseAnalyst(int rowNo)
        {
            XElement analystElement = htmlDocument.XPathSelectElement(XpathGenerator.PicksPageAnalystXpath(rowNo));
            if (analystElement != null)
                return DataSanitizer.GetElementValue(analystElement);
            else
                return "";
        }
    }
}
