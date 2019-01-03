using FightData.Domain;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing
{
    public class RawResultsPageData
    {
        public RawResultsPageData(List<RawFightResult> rawFightResults, string date)
        {
            RawFightResults = rawFightResults;
            Date = date;
        }

        public List<RawFightResult> RawFightResults { get; private set; }

        public string Date { get; private set; }
    }
}
