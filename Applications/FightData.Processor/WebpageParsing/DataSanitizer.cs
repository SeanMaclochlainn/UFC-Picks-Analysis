using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing
{
    public class DataSanitizer
    {
        public static string GetNodeText(HtmlNode node)
        {
            string nodeText = RemoveIrregularCharacters(node.InnerText);
            return nodeText.Trim();
        }

        private static string RemoveIrregularCharacters(string text)
        {
            text = text.Replace("&#160;", " ");
            return text.Replace("&nbsp;", " ");
        }
    }
}
