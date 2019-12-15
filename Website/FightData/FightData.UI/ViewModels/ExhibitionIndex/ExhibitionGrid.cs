using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using System.Collections.Generic;
using System.Linq;

namespace FightData.UI.ViewModels.ExhibitionIndex
{
    public class ExhibitionGrid
    {
        public List<ExhibitionGridEntry> ExhibitionGridEntries { get; private set; } = new List<ExhibitionGridEntry>();
        public List<Website> Websites { get; private set; } = new List<Website>();

        public void LoadViewData(FightPicksContext context)
        {
            EntityFinder entityFinder = new EntityFinder(context);
            foreach (Exhibition exhibition in entityFinder.ExhibitionFinder.FindNewestToOldest())
                ExhibitionGridEntries.Add(new ExhibitionGridEntry(exhibition));
            Websites = entityFinder.WebsiteFinder.GetAllWebsites();
        }
    }
}
