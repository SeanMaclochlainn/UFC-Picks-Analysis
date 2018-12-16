using FightData.Domain.Entities;
using System.Linq;

namespace FightData.UI.ViewModels.ExhibitionIndex
{
    public class ExhibitionGridEntry
    {
        public ExhibitionGridEntry(Exhibition exhibition)
        {
            Exhibition = exhibition;
        }

        public Exhibition Exhibition { get; private set; }

        public string GetWebsiteUrl(Website website)
        {
            return Exhibition.Webpages.Single(w => w.Website == website).Url;
        }

        public bool IsDataDownloaded()
        {
            return !string.IsNullOrEmpty(Exhibition.Webpages.First().Data);
        }

        public bool IsParsed()
        {
            return Exhibition.Webpages.First().Parsed;
        }
    }
}
