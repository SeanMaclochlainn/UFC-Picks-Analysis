using FightData.Domain.Entities;
using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class HtmlPageParser
    {
        private Webpage webpage;

        public HtmlPageParser(Webpage webpage)
        {
            this.webpage = webpage;
        }

        public XDocument ParseHtml()
        {
            return XDocument.Parse(webpage.Data);
        }
    }
}
