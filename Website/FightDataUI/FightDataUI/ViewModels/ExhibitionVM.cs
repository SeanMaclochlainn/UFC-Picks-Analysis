using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightDataUI.ViewModels
{
    public class ExhibitionVM
    {
        public ExhibitionVM(List<Exhibition> exhibitions, FightPicksContext context)
        {
            Exhibitions = exhibitions;
            Context = context;
        }

        public List<Exhibition> Exhibitions { get; private set; }
        public FightPicksContext Context { get; private set; }
    }
}
