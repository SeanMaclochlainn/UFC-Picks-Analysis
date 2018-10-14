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
            this.exhibitionFinder = new ExhibitionFinder(context);
        }

        public List<Exhibition> Exhibitions { get; private set; }
        
        public void LoadData()
        {
            Exhibitions = exhibitionFinder.FindAllExhibitions();
        }

    }
}
