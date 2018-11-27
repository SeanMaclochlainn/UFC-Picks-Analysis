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
        
        public OddsEvaluatorResult GetOddEntities(List<RawFighterOdds> rawFighterOdds, Exhibition exhibition)
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
                        Value = ConvertToDecimalOdd(rawFighterOdd.Odds)
                    };
                    odds.Add(odd);
                }
                else
                    unfoundOdds.Add(rawFighterOdd);
            }
            return new OddsEvaluatorResult(odds, unfoundOdds);
        }

        private decimal ConvertToDecimalOdd(string moneylineOdd)
        {
            char firstCharacter = moneylineOdd[0];
            decimal numericalValue = decimal.Parse(moneylineOdd.TrimStart(new char[] { '+', '-' }));
            if (firstCharacter == '+')
                return Math.Round((numericalValue / 100) + 1, 2);
            else
                return Math.Round((100 / numericalValue) + 1, 2);
        }
    }
}
