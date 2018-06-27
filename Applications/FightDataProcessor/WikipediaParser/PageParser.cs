using FightData.Domain;
using FightData.Domain.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WikipediaParser
{
    public class PageParser
    {
        private Webpage wikipediaWebpage;
        private HtmlDocument wikipediaHtml;
        private UfcEvent ufcEvent;

        public PageParser(Webpage wikipediaWebpage)
        {
            this.wikipediaWebpage = wikipediaWebpage;
            LoadWikipediaHtml();
            ufcEvent = wikipediaWebpage.Event;
        }

        public void ParseResultsTable()
        {
            TableParser tableParser = new TableParser(wikipediaHtml, ufcEvent);
            tableParser.Parse();
        }

        private void LoadWikipediaHtml()
        {
            wikipediaHtml = new HtmlDocument();
            wikipediaHtml.LoadHtml(wikipediaWebpage.Data);
        }



    }
}
