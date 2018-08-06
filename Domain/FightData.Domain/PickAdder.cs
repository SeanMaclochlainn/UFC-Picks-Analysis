using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain
{
    public class PickAdder
    {
        private FightPicksContext context;
        private Exhibition exhibition;
        private string analystName;
        private string fighterName;
        private Analyst analyst;
        private Fighter fighter;
        private Fight fight;

        public PickAdder(Exhibition exhibition)
        {
            context = exhibition.Context;
            this.exhibition = exhibition;
        }
        
        public void AddPicks(List<RawExhibitionPicks> rawExhibitionPicks)
        {
            foreach (RawExhibitionPicks rawExhibitionPick in rawExhibitionPicks)
                AddPicks(rawExhibitionPick);
        }

        public void AddPicks(RawExhibitionPicks rawExhibitionPick)
        {
            foreach (string fighterName in rawExhibitionPick.FighterNames)
                AddPick(rawExhibitionPick.AnalystName, fighterName);
        }

        private void AddPick(string analystName, string fighterName)
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
            return FighterFinder.WithinExhibition(exhibition, context).FindFighter(fighterName).Result;
        }
        
        private Fight GetFight()
        {
            return FightFinder.WithinExhibition(exhibition, context).FindFight(fighter).Result;
        }
    }
}
