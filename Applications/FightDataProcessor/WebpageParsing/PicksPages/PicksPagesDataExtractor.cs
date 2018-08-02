﻿using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPagesDataExtractor
    {
        private PickAdder pickAdder;
        private AnalystFinder analystFinder;
        private UfcEvent ufcEvent;

        public PicksPagesDataExtractor(UfcEvent ufcEvent, FightPicksContext context)
        {
            this.ufcEvent = ufcEvent;
            pickAdder = new PickAdder(ufcEvent, context);
            analystFinder = new AnalystFinder(context);
        }

        public void ExtractAllPages()
        {
            List<Webpage> picksPages = ufcEvent.Webpages.Where(w => w.WebpageType == WebpageType.PicksPage).ToList();
            foreach (Webpage picksPage in picksPages)
            {
                ExtractGridData(XDocument.Parse(picksPage.Data));
            }
        }

        private void ExtractGridData(XDocument picksPageHtml)
        {
            GridParser gridParser = new GridParser(picksPageHtml);
            List<GridRowResult> gridRowResults = gridParser.ParseRows();
            foreach (GridRowResult gridRowResult in gridRowResults)
            {
                if (gridRowResult.IsValidRow())
                    ExtractRowData(gridRowResult);
            }
        }

        private void ExtractRowData(GridRowResult gridRowResult)
        {
            Analyst analyst = analystFinder.FindAnalyst(gridRowResult.AnalystName).Result;
            foreach (string fighterName in gridRowResult.FighterNames)
                pickAdder.AddPick(analyst, fighterName);
        }
    }
}