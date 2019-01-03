using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System;
using System.Collections.Generic;

namespace FightData.Processor.WebpageParsing.OddsPage
{
    public class RawFighterOddsEvaluator
    {
        private FightPicksContext context;
        private EntityFinder entityFinder;

        public RawFighterOddsEvaluator(FightPicksContext context)
        {
            this.context = context;
            entityFinder = new EntityFinder(context);
        }
        
        public EvaluatedOdds EvaluateOdds(List<RawFighterOdds> rawFighterOdds, Exhibition exhibition)
        {
            List<Odd> odds = new List<Odd>();
            List<RawFighterOdds> unfoundOdds = new List<RawFighterOdds>();
            foreach(RawFighterOdds rawFighterOdd in rawFighterOdds)
            {
                FinderResult<Fighter> fighterFinder = entityFinder.FighterFinder.FindFighter(rawFighterOdd.FighterName, exhibition);
                if (fighterFinder.IsFound())
                {
                    Fighter fighter = fighterFinder.Result;
                    Fight fight = entityFinder.FightFinder.FindFight(fighter, exhibition).Result;
                    Odd odd = new Odd(context)
                    {
                        Fight = fight,
                        Fighter = fighter,
                        Value = OddsConverter.ConvertToDecimalOdd(rawFighterOdd.Odds)
                    };
                    odds.Add(odd);
                }
                else
                    unfoundOdds.Add(rawFighterOdd);
            }
            return new EvaluatedOdds(odds, unfoundOdds);
        }
    }
}
