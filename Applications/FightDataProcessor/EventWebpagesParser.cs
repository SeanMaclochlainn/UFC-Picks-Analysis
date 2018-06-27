using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FightData.Domain.Entities;

namespace FightDataProcessor.WikipediaParser
{
    public class EventWebpagesParser
    {
        private UfcEvent ufcEvent;

        public EventWebpagesParser(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ParseWebpages()
        {
            PageParser wikipediaParser = new PageParser(GetWikipediaPage());
            wikipediaParser.ParseResultsTable();
        }

        private Webpage GetWikipediaPage()
        {
            return ufcEvent.Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }


    }
}
