using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class EventsProcessor
    {
        protected List<string> eventFilePaths;
        protected string folderPath;
        protected List<string> analysts = new List<string>() { "Rainer", "Lewis", "Phil", "Fraser", "Mookie", "Zane", "Tim", "Anton", "Josh", "Stephie", "Hutch", "Connor", "Nick", "Victor", "Mike Bohn", "John Morgan", "Dann Stupp", "Brian Garcia", "Ben Fowlkes", "Matt Erickson", "Steven Marrocco", "George Garcia" };
        private List<AnalystPerformance> analystPerformances;
        List<Event> events;

        public EventsProcessor() { }

        public EventsProcessor(string fileNames, string folderPath)
        {
            this.folderPath = folderPath;
            
            //set eventFilePaths list
            List<string> fileNameList = fileNames.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
            eventFilePaths = new List<string>();
            foreach (var fileName in fileNameList)
            {
                eventFilePaths.Add(folderPath + fileName + " wiki.txt");
            }
        }

        public void processEvents()
        {
            //set up workbook
            Application application = new Application();
            application.Visible = true;
            Workbook workBook = application.Workbooks.Add();
            Worksheet workSheet = workBook.Worksheets[1];

            //write each event to excel, and also add it to events list
            events = new List<Event>();
            int startingRow = 0;
            foreach (var eventFilePath in eventFilePaths)
            {
                Event e = new Event(eventFilePath);
                e.writeToExcel(application, workBook, workSheet, startingRow);
                events.Add(e);
                startingRow += e.getNoOfFights() + 3;
            }

            //populate analystperformance list with analysts
            analystPerformances = new List<AnalystPerformance>();
            foreach(var analyst in analysts)
            {
                analystPerformances.Add(new AnalystPerformance(analyst));
            }

            //compute each analysts performance
            foreach (var e in events)
            {
                //Event e = new Event(eventFilePath);
                foreach(var fight in e.getfights())
                {
                    foreach(var analyst in analysts)
                    {
                        var analystPerformance = analystPerformances.Single(i=>i.getAnalystName() == analyst);
                        
                        if(e.getAnalystsPick(fight, analyst) == fight.Winner())
                        {
                            analystPerformance.addWin();
                        }
                        else if(e.getAnalystsPick(fight, analyst)!="")
                        {
                            analystPerformance.addLoss();
                        }
                    }
                }
            }

            //compute each analysts profit and enter each analysts balance after each event
            int rowCount = 1;
            foreach (var e in events)
            {
                int columnCount = 1;
                rowCount += e.getNoOfFights() + 1;
                foreach (var analyst in analysts)
                {
                    var analystPerformance = analystPerformances.Single(i => i.getAnalystName() == analyst);
                    analystPerformance.setBalance(e.getAnalystsPostEventBalance(analyst, analystPerformance.getBalance()));
                    workSheet.Cells[rowCount, columnCount+6] = analystPerformance.getBalance();
                    workSheet.Cells[rowCount, columnCount+6].NumberFormat = "€#,###.00 ";
                    columnCount++;
                }
                rowCount += 2;
            }

            //enter each analysts performance and net profit
            workSheet.Cells[startingRow + 1, 6] = "% Correctly Picked";
            workSheet.Cells[startingRow + 2, 6] = "Analyst's Final Balance";
            int column = 7;
            foreach (var analystPerformance in analystPerformances)
            {
                workSheet.Cells[startingRow + 1, column] = analystPerformance.getPercentageCorrectPicks();
                workSheet.Cells[startingRow + 1, column].NumberFormat = "###,##%";
                workSheet.Cells[startingRow + 2, column] = analystPerformance.getBalance();
                workSheet.Cells[startingRow + 2, column].NumberFormat = "€#,###.00";
                column++;
            }

            //autofit columns
            for(int i=1;i<=28;i++)
            {
                workSheet.Columns[i].AutoFit();
            }

            //save and close workbook
            workBook.SaveAs(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))+@"\Results.xlsx");
            workBook.Close(true);
            application.Quit();
        }
    }
}
