using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WikipediaParser
{
    public class LineDataProcessor
    {
        private LineParser lineParser;

        public LineDataProcessor(LineParser lineParser)
        {
            this.lineParser = lineParser;
        }

        public void AddAnyFightersToDatabase()
        {
            //FighterFinder winnerFinder = new FighterFinder(lineParser.WinnersName);
            //if (!winnerFinder.FighterExists)
            //{
            //    winnerFinder.Fighter.
            //}
                


        }

    }
}
