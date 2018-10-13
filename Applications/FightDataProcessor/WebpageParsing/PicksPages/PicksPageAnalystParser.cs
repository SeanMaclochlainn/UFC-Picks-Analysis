﻿using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageAnalystParser
    {
        private HtmlDocument htmlDocument;

        public PicksPageAnalystParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseAnalyst(int rowNo)
        {
            string xpath = XpathGenerator.PicksPageAnalystXpath(rowNo);
            FinderResult<HtmlNode> analystFinderResult = new FinderResult<HtmlNode>(htmlDocument.DocumentNode.SelectNodes(xpath)?.FirstOrDefault());
            Debug.WriteLine($"Searched with xpath: {xpath} \r\n Successful result: {analystFinderResult.IsFound()}");
            if (analystFinderResult.IsFound())
                return DataSanitizer.GetElementValue(analystFinderResult.Result);
            else
                return "";
        }
    }
}
