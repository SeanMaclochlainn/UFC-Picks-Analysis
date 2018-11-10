using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.WebpageParsing.PicksPages
{
    public class ReconciledPicksAdder
    {
        private FightPicksContext context;
        private PickUpdater pickUpdater;
        private AnalystFinder analystFinder;
        private FighterFinder fighterFinder;
        private FightFinder fightFinder;

        public ReconciledPicksAdder(FightPicksContext context)
        {
            this.context = context;
            pickUpdater = new PickUpdater(context);
            analystFinder = new AnalystFinder(context);
            fighterFinder = new FighterFinder(context);
            fightFinder = new FightFinder(context);
        }

        public void AddReconciledPicks(List<ReconciledPick> reconciledPicks, Exhibition exhibition)
        {
            foreach (ReconciledPick reconciledPick in GetValidPicks(reconciledPicks))
            {
                Analyst analyst = analystFinder.FindAnalyst(reconciledPick.CorrectAnalystId).Result;
                Fighter fighter = fighterFinder.FindFighter(reconciledPick.CorrectFighterPickId).Result;
                Pick pick = new Pick(context)
                {
                    Analyst = analyst,
                    Fighter = fighter,
                    Fight = fightFinder.FindFight(fighter, exhibition).Result
                };
                pickUpdater.AddPick(pick);
            }
        }

        private List<ReconciledPick> GetValidPicks(List<ReconciledPick> reconciledPicks)
        {
            return reconciledPicks.Where(rp => !rp.Cancelled).ToList();
        }
    }
}
