using FightData.Domain.Finders;
using FightDataProcessor.WebpageParsing;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.WebpageParsing.OddsPage
{
    public class OddsPageParser
    {
        private HtmlDocument htmlPage;
        private int maxNoOfRows = 30;

        public OddsPageParser(HtmlDocument htmlPage)
        {
            this.htmlPage = htmlPage;
        }

        public List<RawFighterOdds> Parse()
        {
            List<RawFighterOdds> rawFighterOdds = new List<RawFighterOdds>();
            for(int i= 1;i<=maxNoOfRows;i++)
            {
                FinderResult<HtmlNode> fighterNode = ParseFighter(i);
                FinderResult<HtmlNode> fighterOddsNode = ParseOdds(i);
                if (AreFinderResultsValid(new List<FinderResult<HtmlNode>>() { fighterNode, fighterOddsNode }))
                    rawFighterOdds.Add(new RawFighterOdds(fighterNode.Result.InnerText, fighterOddsNode.Result.InnerText));
            }
            return rawFighterOdds;
        }
        
        private FinderResult<HtmlNode> ParseFighter(int rowNo)
        {
            return new FinderResult<HtmlNode>(htmlPage.DocumentNode.SelectSingleNode(XpathGenerator.OddsPageFighter(rowNo)));
        }

        private FinderResult<HtmlNode> ParseOdds(int rowNo)
        {
            return new FinderResult<HtmlNode>(htmlPage.DocumentNode.SelectSingleNode(XpathGenerator.OddsPageOdds(rowNo)));
        }

        private bool AreFinderResultsValid(List<FinderResult<HtmlNode>> finderResults)
        {
            return !finderResults.Any(fr => fr.IsFound() == false);
        }
    }
}
