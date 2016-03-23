using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //ppv events
            //string fileNames = "ufc 178, ufc 179, ufc 180, ufc 181, ufc 182, ufc 183, ufc 184, ufc 185, ufc 186, ufc 187, ufc 188, ufc 189, ufc 190, ufc 191, ufc 192, ufc 193, ufc 194, ufc 195";
            //all ufc events
            string fileNames = "ufc 178, ufc 179, ufc 180, ufc fight night 57, ufc 181, ufc on fox 13, ufc fight night 58, ufc 182, ufc fight night 59, ufc on fox 14, ufc 183, ufc fight night 60, ufc fight night 61, ufc 184, ufc 185, ufc fight night 62, ufc fight night 63, ufc fight night 64, ufc on fox 15, ufc 186, ufc fight night 65, ufc fight night 66, ufc 187, ufc fight night 67, ufc fight night 68, ufc 188, ufc fight night 69, ufc fight night 70, ufc 189, tuf 21 finale, ufc fight night 71, ufc fight night 72, ufc on fox 16, ufc 190, ufc fight night 73, ufc fight night 74, ufc 191, ufc fight night 75, ufc 192, ufc fight night 76, ufc fight night 77, ufc 193, ufc fight night 78, ufc fight night 79, ufc fight night 80, tuf 22 finale, ufc 194, ufc on fox 17, ufc 195, ufc fight night 81, ufc on fox 18, ufc fight night 82, ufc fight night 83, ufc fight night 84, ufc 196";
            
            string folderPath = Environment.CurrentDirectory+"\\Data\\";
            //string folderPath = "C:\\Users\\Sean\\Programming\\Mmajunkie picks\\";
            EventsProcessor ep = new EventsProcessor(fileNames, folderPath);
            ep.processEvents();
        }

    }
}
