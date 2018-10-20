using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Views
{
    public class PickView
    {
        private FightPicksContext context;
        private ExhibitionFinder exhibitionFinder;

        public PickView(FightPicksContext context)
        {
            this.context = context;
            exhibitionFinder = new ExhibitionFinder(context);
            AnalystFinder = new AnalystFinder(context);
            PickFinder = new PickFinder(context);
        }

        public List<Exhibition> Exhibitions { get; private set; }
        public AnalystFinder AnalystFinder { get; private set; }
        public PickFinder PickFinder { get; private set; }
        
        public void LoadData()
        {
            Exhibitions = exhibitionFinder.FindAllExhibitions();
        }

        public string FindPickText(Analyst analyst, Fight fight)
        {
            FinderResult<Pick> pickFinderResult = PickFinder.FindPick(analyst, fight);
            if (pickFinderResult.IsFound())
                return pickFinderResult.Result.Fighter.LastName;
            else
                return "";
        }

    }
}
