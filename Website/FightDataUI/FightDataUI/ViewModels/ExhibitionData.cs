using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightDataUI.ViewModels
{
    public class ExhibitionData
    {
        public string Name { get; set; }
        public List<Webpage> Webpages { get; set; } = new List<Webpage>();
    }
}
