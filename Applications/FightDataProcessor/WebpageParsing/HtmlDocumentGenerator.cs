using FightData.Domain.Entities;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing
{
    public class HtmlDocumentGenerator
    {
        private HtmlDocumentGenerator(Webpage webpage)
        {
            HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(webpage.Data);
        }

        public static HtmlDocumentGenerator FromWebpage(Webpage webpage)
        {
            return new HtmlDocumentGenerator(webpage);
        }

        public HtmlDocument HtmlDocument { get; private set; }
    }
}
