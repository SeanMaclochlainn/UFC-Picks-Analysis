using FightData.Domain.Entities;
using HtmlAgilityPack;

namespace FightDataProcessor
{
    public class HtmlDocumentGenerator
    {
        public HtmlDocumentGenerator(Webpage webpage)
        {
            HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(webpage.Data);
        }

        public HtmlDocument HtmlDocument { get; private set; }
    }
}
