using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class PicksCollector
    {
        private string analystName;
        private List<string> fighterNames;
        private UfcEvent ufcEvent;
        private Analyst analyst;
        private List<Fighter> Fighters;
        

        public PicksCollector(GridRowResult gridRowResult, UfcEvent ufcEvent)
        {
            analystName = gridRowResult.AnalystName;
            fighterNames = gridRowResult.FighterNames;
            this.ufcEvent = ufcEvent;
        }

        public void AddPick(string analystName, List<string> fighterNames)
        {

        }

        //public List<Pick> GetPicks()
        //{
        //    GetAnalyst();
        //}

        private Analyst GetAnalyst()
        {
            AnalystFinder analystFinder = new AnalystFinder(analystName);
            if (analystFinder.AnalystExists)
                analyst = analystFinder.Analyst;
            return analyst;
        }

        //private List<Fighter> GetFighters()
        //{
        //    FighterFinder fighterFinder = new FighterFinder()
        //}
    }
}
