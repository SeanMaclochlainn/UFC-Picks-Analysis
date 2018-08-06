using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class ExhibitionDataExtractor
    {
        private Exhibition exhibition;

        public ExhibitionDataExtractor(Exhibition exhibition)
        {
            this.exhibition = exhibition;
        }

        public void ExtractAllWebpages()
        {
            ExtractResultsPageData();
            ExtractPicksPagesData();
        }

        public void ExtractResultsPageData()
        {
            ResultsPageParser resultsPageParser = new ResultsPageParser(exhibition.GetResultsPage().GetHtml());
            new FightAdder(exhibition).AddFights(resultsPageParser.ParseResultTable());
        }

        public void ExtractPicksPagesData()
        {
            foreach (Webpage picksPage in exhibition.GetPicksPages())
            {
                List<RawExhibitionPicks> rawExhibitionPicks = new PicksPageParser(picksPage.GetHtml()).ParsePicksGrid();
                new PickAdder(exhibition).AddPicks(rawExhibitionPicks);
            }
        }
    }
}
