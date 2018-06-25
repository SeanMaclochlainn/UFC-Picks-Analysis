using FightData.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class Parser
    {
        private Webpage wikipediaWebpage;
        private HtmlDocument wikipediaPage;

        public Parser(Webpage wikipediaWebpage)
        {
            this.wikipediaWebpage = wikipediaWebpage;
            this.wikipediaPage = new HtmlDocument();
            wikipediaPage.LoadHtml(wikipediaWebpage.Data);
        }

        public void Parse()
        {

            
            

        }



    }
}
