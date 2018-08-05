using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain
{
    public class PickAdder
    {
        private FightPicksContext context;
        private UfcEvent ufcEvent;
        private string analystName;
        private string fighterName;
        private Analyst analyst;
        private Fighter fighter;
        private Fight fight;

        public PickAdder(UfcEvent ufcEvent)
        {
            context = ufcEvent.Context;
            this.ufcEvent = ufcEvent;
        }
        
        public void AddPicks(List<RawUfcEventPicks> rawUfcEventPicks)
        {
            foreach (RawUfcEventPicks rawUfcEventPick in rawUfcEventPicks)
                AddPicks(rawUfcEventPick);
        }

        public void AddPicks(RawUfcEventPicks rawUfcEventPick)
        {
            foreach (string fighterName in rawUfcEventPick.FighterNames)
                AddPick(rawUfcEventPick.AnalystName, fighterName);
        }

        public void AddPick(string analystName, string fighterName)
        {
            this.analystName = analystName;
            this.fighterName = fighterName;
            AddPick();
        }

        private void AddPick()
        {
            analyst = GetAnalyst();
            fighter = GetFighter();
            fight = GetFight();
            Pick pick = new Pick(context)
            {
                Analyst = analyst,
                Fighter = fighter,
                Fight = fight
            };
            pick.Add();
        }

        private Analyst GetAnalyst()
        {
            return new AnalystFinder(context).FindAnalyst(analystName).Result;
        }

        private Fighter GetFighter()
        {
            return FighterFinder.WithinEvent(ufcEvent, context).FindFighter(fighterName).Result;
        }
        
        private Fight GetFight()
        {
            return FightFinder.WithinEvent(ufcEvent, context).FindFight(fighter).Result;
        }
    }
}
