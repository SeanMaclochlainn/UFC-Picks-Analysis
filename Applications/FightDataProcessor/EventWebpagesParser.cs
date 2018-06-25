using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightDataProcessor
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
            Parser wikipediaParser = new Parser(GetWikipediaPage());
            wikipediaParser.Parse();
        }

        private Webpage GetWikipediaPage()
        {
            return ufcEvent.Webpages.Single(w => w.Website.WebsiteName == WebsiteName.Wikipedia);
        }


    }
}
