
using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class GridRowParser
    {
        private HtmlDocument htmlDocument;
        private int rowNo;
        private AnalystParser analystParser;
        private FightersParser fightersParser;

        public GridRowParser(HtmlDocument htmlDocument, int rowNo)
        {
            this.htmlDocument = htmlDocument;
            this.rowNo = rowNo;
            InitializeParsers();
            LoadResults();
        }

        public GridRowResult GridRowResult { get; private set; }

        public bool IsValidRow()
        {
            return analystParser.IsValidRow();
        }

        private void InitializeParsers()
        {
            analystParser = new AnalystParser(htmlDocument, rowNo);
            fightersParser = new FightersParser(htmlDocument, rowNo);
        }

        private void LoadResults()
        {
            if(IsValidRow())
            {
                GridRowResult gridRowResult = new GridRowResult(analystParser.AnalystName, fightersParser.FighterNames);
                GridRowResult = gridRowResult;
            }
        }




    }
}
