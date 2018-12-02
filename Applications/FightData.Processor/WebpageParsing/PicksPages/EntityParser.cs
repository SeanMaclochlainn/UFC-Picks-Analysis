using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class EntityParser
    {
        private HtmlDocument htmlDocument;

        public EntityParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseEntity(int rowNo, string xpath)
        {
            FinderResult<HtmlNode> analystFinderResult = new FinderResult<HtmlNode>(htmlDocument.DocumentNode.SelectNodes(XpathGenerator.FormatXpath(xpath, rowNo))?.FirstOrDefault());
            Debug.WriteLine($"Searched for analyst with xpath: {xpath} \r\n Successful result: {analystFinderResult.IsFound()}");
            if (analystFinderResult.IsFound())
            {
                string analystName = DataSanitizer.GetElementValue(analystFinderResult.Result);
                Debug.WriteLine($"Found analyst {analystName}");
                return analystName;
            }
            else
                return "";
        }
    }
}
