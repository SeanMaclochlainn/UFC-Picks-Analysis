using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class EventsParser
    {
        private UfcEventFinder ufcEventFinder;

        public EventsParser()
        {
            ufcEventFinder = new UfcEventFinder();
        }

        public void ParseEvents()
        {
            foreach (UfcEvent ufcEvent in ufcEventFinder.GetAllEvents())
            {
                EventWebpagesParser eventWebpagesParser = new EventWebpagesParser(ufcEvent);
                eventWebpagesParser.ParseWebpages();
            }
        }
    }
}
