using HtmlAgilityPack;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class DataSanitizer
    {
        public static string GetElementValue(HtmlNode node)
        {
            return node.InnerText.Trim();
        }
    }
}
