using FightData.Domain;
using FightData.Domain.Entities;
using System.Collections.Generic;

namespace FightData.UI.ViewModels
{
    public class UnfoundPicksView
    {
        public UnfoundPicksView(List<RawAnalystPick> invalidPicks, Exhibition exhibition)
        {
            UnfoundPicks = invalidPicks;
            Exhibition = exhibition;
        }

        public List<RawAnalystPick> UnfoundPicks { get; private set; }
        public Exhibition Exhibition { get; private set; }
    }
}
