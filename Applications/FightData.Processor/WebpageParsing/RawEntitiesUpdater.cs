using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.Processor.WebpageParsing.PicksPages;
using FightDataProcessor.PicksPages.WebpageParsing;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing
{
    public class RawEntitiesUpdater
    {
        private FightPicksContext context;
        private RawExhibitionEntities rawExhibitionEntities;
        private Exhibition exhibition;
        private PickUpdater pickUpdater;
        private OddUpdater oddUpdater;

        public RawEntitiesUpdater(FightPicksContext context)
        {
            this.context = context;
            pickUpdater = new PickUpdater(context);
            oddUpdater = new OddUpdater(context); 
        }

        public UpdateEntitiesResult UpdateEntities(RawExhibitionEntities rawExhibitionEntities, Exhibition exhibition)
        {
            this.rawExhibitionEntities = rawExhibitionEntities;
            this.exhibition = exhibition;
            AddRawFightResults();
            EvaluatedPicks evaluatedPicks = EvaluatePicks();
            EvaluatedOdds evaluatedOdds = EvaluateOdds();
            pickUpdater.AddPicks(evaluatedPicks.ValidPicks);
            oddUpdater.AddOdds(evaluatedOdds.ValidOdds);
            return new UpdateEntitiesResult(evaluatedPicks.UnfoundPicks, evaluatedOdds.UnfoundOdds);
        }

        private void AddRawFightResults()
        {
            new FightUpdater(context).AddFights(rawExhibitionEntities.RawFightResults, exhibition);
        }

        private EvaluatedPicks EvaluatePicks()
        {
            RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
            return rawPickEvaluator.EvaluatePicks(rawExhibitionEntities.RawAnalystPicks, exhibition);
        }

        private EvaluatedOdds EvaluateOdds()
        {
            RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);
            return rawFighterOddsEvaluator.EvaluateOdds(rawExhibitionEntities.RawFighterOdds, exhibition);
        }

        private void AddPicks(List<Pick> picks)
        {
            pickUpdater.AddPicks(picks);
        }

    }
}
