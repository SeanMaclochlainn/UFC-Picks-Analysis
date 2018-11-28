using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.WebpageParsing.PicksPages;
using System.Collections.Generic;

namespace FightDataProcessor.PicksPages.WebpageParsing
{
    public class RawPickEvaluator
    {
        private PickUpdater pickUpdater;
        private FightPicksContext context;
        private Exhibition exhibition;
        private string analystName;
        private string fighterName;
        FinderResult<Analyst> analystFinderResult;
        FinderResult<Fighter> fighterFinderResult;
        FinderResult<Fight> fightFinderResult;

        public RawPickEvaluator(FightPicksContext context)
        {
            pickUpdater = new PickUpdater(context);
            this.context = context;
            analystFinderResult = new FinderResult<Analyst>(null);
            fighterFinderResult = new FinderResult<Fighter>(null);
            fightFinderResult = new FinderResult<Fight>(null);
            analystName = "";
            fighterName = "";
        }

        public EvaluatedPicks EvaluatePicks(List<RawAnalystPick> rawAnalystPickList, Exhibition exhibition)
        {
            this.exhibition = exhibition;
            List<UnfoundPick> unfoundPicks = new List<UnfoundPick>();
            List<Pick> validPicks = new List<Pick>();
            foreach (RawAnalystPick analystPick in rawAnalystPickList)
            {
                analystName = analystPick.Analyst;
                fighterName = analystPick.Pick;
                if (ArePicksValid())
                {
                    FindEntities();
                    if (AreEntitiesFound())
                        validPicks.Add(new Pick(context) { Analyst = analystFinderResult.Result, Fight = fightFinderResult.Result, Fighter = fighterFinderResult.Result });
                    else
                        unfoundPicks.Add(new UnfoundPick(analystPick, analystFinderResult.IsFound(), fighterFinderResult.IsFound()));
                }
            }
            return new EvaluatedPicks(unfoundPicks, validPicks);
        }

        private void FindEntities()
        {
            analystFinderResult = new AnalystFinder(context).FindAnalyst(analystName);
            fighterFinderResult = new FighterFinder(context).FindFighter(fighterName, exhibition);
            if (fighterFinderResult.IsFound())
                fightFinderResult = new FightFinder(context).FindFight(fighterFinderResult.Result, exhibition);
        }

        private bool AreEntitiesFound()
        {
            return analystFinderResult.IsFound() && fighterFinderResult.IsFound() && fightFinderResult.IsFound();
        }

        private bool ArePicksValid()
        {
            return !string.IsNullOrEmpty(analystName);
        }
    }
}
