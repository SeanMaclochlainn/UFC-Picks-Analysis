using FightData.Domain.Entities;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing
{
    public class HtmlDocumentGenerator
    {
        public static HtmlDocument FromWebpage(Webpage webpage)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(webpage.Data);
            return htmlDocument;
        }
    }
}
