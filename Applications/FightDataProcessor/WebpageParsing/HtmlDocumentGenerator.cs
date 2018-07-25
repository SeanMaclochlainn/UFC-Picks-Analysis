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
    //public class HtmlDocumentGenerator
    //{
    //    private HtmlDocumentGenerator(Webpage webpage)
    //    {
    //        HtmlDocument = new HtmlDocument();
    //        HtmlDocument.LoadHtml(webpage.Data);
    //    }

    //    public static HtmlDocumentGenerator FromWebpage(Webpage webpage)
    //    {
    //        return new HtmlDocumentGenerator(webpage);
    //    }

    //    public HtmlDocument HtmlDocument { get; private set; }
    //}
}
