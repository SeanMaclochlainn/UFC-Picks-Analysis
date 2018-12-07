using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Linq;
using System.Text.RegularExpressions;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class EntityParser
    {
        private HtmlDocument htmlDocument;
        private string regex;

        public EntityParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseEntity(int row, string xpath, string regex)
        {
            this.regex = regex;
            FinderResult<HtmlNode> nodeFinderResult = new FinderResult<HtmlNode>(htmlDocument.DocumentNode.SelectNodes(XpathGenerator.FormatXpath(xpath, row))?.FirstOrDefault());
            if (nodeFinderResult.IsFound())
            {
                return ExtractNodeText(nodeFinderResult.Result);
            }
            else
                return "";
        }

        private string ExtractNodeText(HtmlNode node)
        {
            string nodeText = DataSanitizer.GetNodeText(node);
            if (IsRegexApplicable())
                return ApplyRegex(nodeText);
            else
                return nodeText;
        }

        private string ApplyRegex(string text)
        {
            Match match = Regex.Match(text, regex);
            return match.Groups.Last().Value;
        }

        private bool IsRegexApplicable()
        {
            return !string.IsNullOrEmpty(regex);
        }
    }
}
