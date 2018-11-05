using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing
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

        public List<RawAnalystPick> InvalidPicks { get; private set; } = new List<RawAnalystPick>();
        public List<Pick> ValidPicks { get; private set; } = new List<Pick>();

        public void EvaluatePicks(List<RawAnalystPick> rawAnalystPicks, Exhibition exhibition)
        {
            this.exhibition = exhibition;
            foreach (RawAnalystPick analystPick in rawAnalystPicks)
            {
                analystName = analystPick.Analyst;
                fighterName = analystPick.Pick;
                FindEntities();
                if (FinderEntitiesValid())
                    ValidPicks.Add(new Pick(context) { Analyst = analystFinderResult.Result, Fight = fightFinderResult.Result, Fighter = fighterFinderResult.Result });
                else
                    InvalidPicks.Add(analystPick);
            }
        }

        private void FindEntities()
        {
            analystFinderResult = new AnalystFinder(context).FindAnalyst(analystName);
            fighterFinderResult = new FighterFinder(context).FindFighter(fighterName, exhibition);
            if (fighterFinderResult.IsFound())
                fightFinderResult = new FightFinder(context).FindFight(fighterFinderResult.Result, exhibition);
        }

        private bool FinderEntitiesValid()
        {
            return analystFinderResult.IsFound() && fighterFinderResult.IsFound() && fightFinderResult.IsFound();
        }
    }
}
