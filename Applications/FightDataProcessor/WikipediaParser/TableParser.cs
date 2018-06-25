using FightData.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WikipediaParser
{
    public class TableParser
    {
        private HtmlDocument document;
        
        public TableParser(HtmlDocument document)
        {
            this.document = document;
        }

        public void Parse()
        {
            for (int lineCount = 1; lineCount <= 20; lineCount++)
            {
                LineParser lineParser = new LineParser(document, lineCount);
                if(lineParser.ValidLine())
                {
                    //FighterFinder fighterFinder = new FighterFinder(lineParser.WinnersName);
                    //if(fighterFinder.FighterExists)

                        
                }
                
            }

        }

    }
}
