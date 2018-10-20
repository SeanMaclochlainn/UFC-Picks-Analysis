using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing
{
    public class HtmlPageParser
    {
        private string html;

        public HtmlPageParser(string html)
        {
            this.html = html;
        }

        public HtmlDocument ParseHtml()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }
    }
}
