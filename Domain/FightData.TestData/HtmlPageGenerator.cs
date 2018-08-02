using System;
using System.IO;
using System.Reflection;

namespace FightData.TestData
{
    public class HtmlPageGenerator
    {
        public static string GetWikipediaPage()
        {
            return GetResourceFile("ResultsPage.html");
        }

        public static string GetPicksPage()
        {
            return GetResourceFile("PicksPage.html");
        }

        private static string GetResourceFile(string fileName)
        {
            string fileData = "";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string currentAssemblyName = currentAssembly.GetName().Name;
            string folderName = "WebsiteHtml";
            using (Stream stream = currentAssembly.GetManifestResourceStream(String.Format("{0}.{1}.{2}", currentAssemblyName, folderName, fileName)))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    fileData = sr.ReadToEnd();
                }
            }
            return fileData;
        }
        
    }
}
