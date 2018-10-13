using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Diagnostics;

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
            Webpage resultsPage = exhibition.GetResultsPage();
            Debug.WriteLine($"Parsing results page {resultsPage.Url}");
            ResultsPageParser resultsPageParser = new ResultsPageParser(new HtmlPageParser(resultsPage.Data).ParseHtml());
            new FightAdder(exhibition).AddFights(resultsPageParser.ParseResultTable());
        }

        public void ExtractPicksPagesData()
        {
            foreach (Webpage picksPage in exhibition.GetPicksPages())
            {
                Debug.WriteLine($"Parsing picks page {picksPage.Url}");
                List<RawExhibitionPicks> rawExhibitionPicks = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml()).ParsePicksGrid();
                new PickAdder(exhibition).AddPicks(rawExhibitionPicks);
            }
        }
    }
}
