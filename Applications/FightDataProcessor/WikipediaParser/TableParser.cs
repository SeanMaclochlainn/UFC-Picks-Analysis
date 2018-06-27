using FightData.Domain;
using FightData.Domain.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WikipediaParser
{
    public class TableParser
    {
        private HtmlDocument document;
        private EventDataUpdater eventDataUpdater;

        public TableParser(HtmlDocument document, UfcEvent ufcEvent)
        {
            this.document = document;
            eventDataUpdater = new EventDataUpdater(ufcEvent);
        }

        public void Parse()
        {
            for (int i = 1; i <= 20; i++)
            {
                ProcessRow(i);
            }
        }

        private void ProcessRow(int rowNo)
        {
            TableRowParser tableRowParser = new TableRowParser(document, rowNo);
            if (tableRowParser.IsValidRow())
            {
                eventDataUpdater.AddFightData(tableRowParser.WinnersName, tableRowParser.LosersName);
            }
        }

    }
}
