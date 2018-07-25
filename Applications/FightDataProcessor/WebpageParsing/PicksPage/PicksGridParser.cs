using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class PicksGridParser
    {
        private HtmlDocument htmlDocument;
        private int maxNoOfGridRows = 20;
        private FightPicksContext context;
        private PickAdder pickAdder;

        public PicksGridParser(HtmlDocument htmlDocument, UfcEvent ufcEvent, FightPicksContext context)
        {
            this.htmlDocument = htmlDocument;
            pickAdder = new PickAdder(ufcEvent, context);
        }

        public void Parse()
        {
            for (int rowNo = 0; rowNo <= maxNoOfGridRows; rowNo++)
            {
                GridRowParser gridRowParser = new GridRowParser(htmlDocument, rowNo);
                if(gridRowParser.IsValidRow())
                {
                    string analystName = gridRowParser.GridRowResult.AnalystName;
                    List<string> fightersNames = gridRowParser.GridRowResult.FighterNames;
                    foreach (string fighterNames in fightersNames)
                        pickAdder.AddPick(analystName, fighterNames);
                }
            }
        }
    }
}
