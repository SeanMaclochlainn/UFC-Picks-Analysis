using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing
{
    public class DataSanitizer
    {
        public static string GetNodeText(HtmlNode node)
        {
            return node.InnerText.Trim();
        }
    }
}
