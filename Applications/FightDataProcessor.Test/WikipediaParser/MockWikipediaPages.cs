using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FightDataProcessor.Test.WikipediaParser
{
    public class MockWikipediaPages
    {
        public static string GetStandardPage()
        {
            return GetResourceFile("FN55Wikipedia.html");
        }

        private static string GetResourceFile(string fileName)
        {
            string fileData = "";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string currentAssemblyName = currentAssembly.GetName().Name;
            string folderName = "WebsiteData";
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
