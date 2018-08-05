using FightData.Domain;
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

        public PicksPagesDataExtractor(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
            pickAdder = new PickAdder(ufcEvent);
            analystFinder = new AnalystFinder(ufcEvent.Context);
        }

        public void ExtractAllPages()
        {
            List<Webpage> picksPages = ufcEvent.Webpages.Where(w => w.WebpageType == WebpageType.PicksPage).ToList();
            foreach (Webpage picksPage in picksPages)
            {
                ExtractGridData(picksPage.GetHtml());
            }
        }

        private void ExtractGridData(XDocument picksPageHtml)
        {
            PicksPageGridParser picksPageGridParser = new PicksPageGridParser(picksPageHtml);
            List<ParsedGridRow> gridRowResults = picksPageGridParser.ParseRows();
            foreach (ParsedGridRow gridRowResult in gridRowResults)
                ExtractRowData(gridRowResult);
        }

        private void ExtractRowData(ParsedGridRow gridRowResult)
        {
            Analyst analyst = analystFinder.FindAnalyst(gridRowResult.AnalystName).Result;
            foreach (string fighterName in gridRowResult.FighterNames)
                pickAdder.AddPick(analyst, fighterName);
        }
    }
}
