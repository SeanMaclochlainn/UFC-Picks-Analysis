using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class EntityRowParser
    {
        private HtmlDocument htmlDocument;
        private int row;
        private string regex;
        private string xpath;

        public EntityRowParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public List<string> ParseEntities(int row, string xpath, string regex)
        {
            this.row = row;
            this.regex = regex;
            this.xpath = xpath;
            return GetRowEntities();
        }

        private static bool IsRegexApplicable(string regex)
        {
            return !string.IsNullOrEmpty(regex);
        }

        private static bool IsValidEntity(string entity)
        {
            return !string.IsNullOrEmpty(entity);
        }

        private List<string> GetRowEntities()
        {
            List<string> entities = new List<string>();
            bool entityFound = true;
            int column = 1;
            while (entityFound)
            {
                FinderResult<HtmlNode> findNodeResult = FindRowNode(column);
                if (findNodeResult.IsFound())
                {
                    string entity = ExtractNodeText(findNodeResult.Result, column);
                    if (IsValidEntity(entity))
                    {
                        entities.Add(entity);
                        column++;
                    }
                    else
                        entityFound = false;
                }
                else
                    entityFound = false;
            }
            return entities;
        }

        private string ExtractNodeText(HtmlNode node, int column)
        {
            string nodeText = DataSanitizer.GetNodeText(node);
            if (IsRegexApplicable(regex))
                return ApplyRegex(nodeText, column);
            else
                return nodeText;
        }

        private FinderResult<HtmlNode> FindRowNode(int column)
        {
            return new FinderResult<HtmlNode>(htmlDocument.DocumentNode.SelectNodes(XpathGenerator.FormatXpath(xpath, row, column))?.FirstOrDefault());
        }

        private string FormatRegex(int column)
        {
            return regex.Replace("{column-incrementer}", column.ToString());
        }

        private string ApplyRegex(string text, int column)
        {
            Match match = Regex.Match(text, FormatRegex(column));
            return match.Groups.Last().Value;
        }
    }
}
