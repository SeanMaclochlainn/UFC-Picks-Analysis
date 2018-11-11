using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;

namespace FightData.UI.ViewModels
{
    public class ExhibitionIndexView
    {
        public ExhibitionIndexView()
        {
            Exhibitions = new List<Exhibition>();
            Websites = new List<Website>();
        }

        public List<Exhibition> Exhibitions { get; private set; }
        public List<Website> Websites { get; private set; }

        public void LoadViewData(FightPicksContext context)
        {
            Websites = new WebsiteFinder(context).GetAllWebsites();
            Exhibitions = new ExhibitionFinder(context).FindAllExhibitions();
        }
        
    }
}
