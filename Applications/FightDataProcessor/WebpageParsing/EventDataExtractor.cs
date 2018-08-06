﻿using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class EventDataExtractor
    {
        private UfcEvent ufcEvent;

        public EventDataExtractor(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ExtractAllWebpages()
        {
            ExtractResultsPageData();
            ExtractPicksPagesData();
        }

        public void ExtractResultsPageData()
        {
            ResultsPageParser resultsPageParser = new ResultsPageParser(ufcEvent.GetResultsPage().GetHtml());
            new FightAdder(ufcEvent).AddFights(resultsPageParser.ParseResultTable());
        }

        public void ExtractPicksPagesData()
        {
            foreach (Webpage picksPage in ufcEvent.GetPicksPages())
            {
                List<RawUfcEventPicks> rawUfcEventPicks = new PicksPageParser(picksPage.GetHtml()).ParsePicksGrid();
                new PickAdder(ufcEvent).AddPicks(rawUfcEventPicks);
            }
        }
    }
}
