using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.Processor.WebpageParsing.PicksPages;
using FightDataProcessor.PicksPages.WebpageParsing;
using System;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing
{
    public class RawEntitiesUpdater
    {
        private FightPicksContext context;
        private RawExhibitionData rawExhibitionData;
        private Exhibition exhibition;
        private PickUpdater pickUpdater;
        private OddUpdater oddUpdater;
        private ExhibitionUpdater exhibitionUpdater;

        public RawEntitiesUpdater(FightPicksContext context)
        {
            this.context = context;
            pickUpdater = new PickUpdater(context);
            oddUpdater = new OddUpdater(context);
            exhibitionUpdater = new ExhibitionUpdater(context);
        }

        public UpdateEntitiesResult UpdateEntities(RawExhibitionData rawExhibitionData, Exhibition exhibition)
        {
            this.rawExhibitionData = rawExhibitionData;
            this.exhibition = exhibition;
            UpdateExhibitionDate();
            AddRawFightResults();
            EvaluatedPicks evaluatedPicks = EvaluatePicks();
            EvaluatedOdds evaluatedOdds = EvaluateOdds();
            pickUpdater.AddPicks(evaluatedPicks.ValidPicks);
            oddUpdater.AddOdds(evaluatedOdds.ValidOdds);
            return new UpdateEntitiesResult(evaluatedPicks.UnfoundPicks, evaluatedOdds.UnfoundOdds);
        }

        private void UpdateExhibitionDate()
        {
            string dateText = rawExhibitionData.ResultsPageData.Date;
            if (!string.IsNullOrEmpty(dateText))
            {
                DateTime date = DateTime.Parse(dateText);
                exhibitionUpdater.UpdateDate(exhibition, date);
            }
        }

        private void AddRawFightResults()
        {
            new FightUpdater(context).AddFights(rawExhibitionData.ResultsPageData.RawFightResults, exhibition);
        }

        private EvaluatedPicks EvaluatePicks()
        {
            RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
            return rawPickEvaluator.EvaluatePicks(rawExhibitionData.RawAnalystPicks, exhibition);
        }

        private EvaluatedOdds EvaluateOdds()
        {
            RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);
            return rawFighterOddsEvaluator.EvaluateOdds(rawExhibitionData.RawFighterOdds, exhibition);
        }

        private void AddPicks(List<Pick> picks)
        {
            pickUpdater.AddPicks(picks);
        }

    }
}
