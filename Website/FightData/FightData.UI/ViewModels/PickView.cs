using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.UI.ViewModels
{
    public class PickView
    {
        private FightPicksContext context;

        public PickView(FightPicksContext context)
        {
            this.context = context;
            EntityFinder = new EntityFinder(context);
        }

        public List<Exhibition> Exhibitions { get; private set; }
        public EntityFinder EntityFinder { get; private set; }
        
        public void LoadData()
        {
            Exhibitions = EntityFinder.ExhibitionFinder.FindNewestToOldest();
        }

        public string FindPickText(Analyst analyst, Fight fight)
        {
            FinderResult<Pick> pickFinderResult = EntityFinder.PickFinder.FindPick(analyst, fight);
            if (pickFinderResult.IsFound())
                if (pickFinderResult.Result.IsCorrect())
                    return "1";
                else
                    return "0";
            else
                return "";
        }

        public string FindFighterOddText(Fighter fighter, Exhibition exhibition)
        {
            FinderResult<Odd> oddsFinderResult = EntityFinder.OddsFinder.FindFighterOdd(fighter, exhibition);
            if (oddsFinderResult.IsFound())
                return oddsFinderResult.Result.Value.ToString();
            else
                return "";
        }

    }
}
