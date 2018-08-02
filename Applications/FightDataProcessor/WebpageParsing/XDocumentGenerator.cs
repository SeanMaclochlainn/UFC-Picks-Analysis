using FightData.Domain.Entities;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class XDocumentGenerator
    {
        public static XDocument FromWebpage(Webpage webpage)
        {
            return XDocument.Parse(webpage.Data);
        }
    }
}
