using System.Xml.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class DataSanitizer
    {
        public static string GetElementValue(XElement element)
        {
            return element.Value.Trim();
        }
    }
}
