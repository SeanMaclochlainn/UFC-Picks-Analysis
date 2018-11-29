using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.Processor.WebpageParsing.Reconciliation;

namespace FightData.Processor.WebpageParsing.PicksPages
{
    public class ReconciledEntitiesUpdater
    {
        private FightPicksContext context;
        private PickUpdater pickUpdater;
        private AnalystFinder analystFinder;
        private FighterFinder fighterFinder;
        private FightFinder fightFinder;
        private OddUpdater oddUpdater;
        private ReconciledEntities reconciledEntities;
        private Exhibition exhibition;

        public ReconciledEntitiesUpdater(FightPicksContext context)
        {
            this.context = context;
            pickUpdater = new PickUpdater(context);
            analystFinder = new AnalystFinder(context);
            fighterFinder = new FighterFinder(context);
            fightFinder = new FightFinder(context);
            oddUpdater = new OddUpdater(context);
        }

        public void AddReconciledEntities(ReconciledEntities reconciledEntities, Exhibition exhibition)
        {
            this.reconciledEntities = reconciledEntities;
            this.exhibition = exhibition;
            AddReconciledPicks();
            AddReconciledOdds();
        }

        public void AddReconciledPicks()
        {
            foreach (ReconciledPick reconciledPick in reconciledEntities.ReconciledPicks)
            {
                Analyst analyst = analystFinder.FindAnalyst(reconciledPick.CorrectAnalystId).Result;
                Fighter fighter = fighterFinder.FindFighter(reconciledPick.CorrectFighterId).Result;
                Pick pick = new Pick(context)
                {
                    Analyst = analyst,
                    Fighter = fighter,
                    Fight = fightFinder.FindFight(fighter, exhibition).Result
                };
                pickUpdater.AddPick(pick);
            }
        }

        private void AddReconciledOdds()
        {
            foreach (ReconciledOdd reconciledOdd in reconciledEntities.ReconciledOdds)
            {
                Odd odd = new Odd(context);
                Fighter fighter = fighterFinder.FindFighter(reconciledOdd.CorrectFighterId).Result;
                odd.Fight = fightFinder.FindFight(fighter, exhibition).Result;
                odd.Fighter = fighter;
                odd.Value = OddsConverter.ConvertToDecimalOdd(reconciledOdd.RawOdds);
                oddUpdater.AddOdd(odd);
            }
        }
    }
}
