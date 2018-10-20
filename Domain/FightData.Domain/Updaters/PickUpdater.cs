﻿using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.Domain
{
    public class PickUpdater
    {
        private FightPicksContext context;
        private Exhibition exhibition;
        private string analystName;
        private string fighterName;
        FinderResult<Analyst> analystFinderResult;
        FinderResult<Fighter> fighterFinderResult;
        FinderResult<Fight> fightFinderResult;

        public PickUpdater(Exhibition exhibition)
        {
            context = exhibition.Context;
            this.exhibition = exhibition;
            analystFinderResult = new FinderResult<Analyst>(null);
            fighterFinderResult = new FinderResult<Fighter>(null);
            fightFinderResult = new FinderResult<Fight>(null);
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
            FindEntities();
            if (AreEntitiesValid())
            {
                Pick pick = new Pick(context)
                {
                    Analyst = analystFinderResult.Result,
                    Fighter = fighterFinderResult.Result,
                    Fight = fightFinderResult.Result
                };
                pick.Add();
            }

        }

        private void FindEntities()
        {
            FindAnalyst();
            FindFighter();
            FindFight();
        }

        private void FindAnalyst()
        {
            analystFinderResult = new AnalystFinder(context).FindAnalyst(analystName);
        }

        private void FindFighter()
        {
            fighterFinderResult = new FighterFinder(context).FindFighter(fighterName, exhibition);
        }

        private void FindFight()
        {
            if (fighterFinderResult.IsFound())
                fightFinderResult = new FightFinder(context).FindFight(fighterFinderResult.Result, exhibition);
        }

        private bool AreEntitiesValid()
        {
            return analystFinderResult.IsFound() && fighterFinderResult.IsFound() && fightFinderResult.IsFound();
        }
    }
}