using FightData.Domain;
using FightData.Domain.Entities;
using FightData.UI.Utilities;
using FightData.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightDataUI.Controllers
{
    public class PicksController : Controller
    {
        private FightPicksContext context;

        public PicksController(FightPicksContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View(GetPickView());
        }

        public FileContentResult ExportToCsv()
        {
            PickView pickView = GetPickView();
            CsvGenerator csvGenerator = new CsvGenerator();
            List<string> headerEntries = new List<string>() { "Name", "Date", "Winner", "Odds", "Loser", "Odds" };
            headerEntries.AddRange(pickView.EntityFinder.AnalystFinder.GetAllAnalysts().Select(a => a.Name).ToList());
            csvGenerator.AddRow(headerEntries);

            foreach (Exhibition exhibition in pickView.Exhibitions)
            {   
                for (int i = 0; i < exhibition.Fights.Count; i++)
                {
                    List<string> rowEntries = new List<string>();
                    if (i == 0)
                    {
                        rowEntries.Add(exhibition.Name);
                        rowEntries.Add(exhibition.Date.ToShortDateString());
                    }
                    else
                    {
                        rowEntries.Add("");
                        rowEntries.Add("");
                    }
                    rowEntries.Add(exhibition.Fights[i].Winner.FullName);
                    rowEntries.Add(pickView.FindFighterOddText(exhibition.Fights[i].Winner, exhibition));
                    rowEntries.Add(exhibition.Fights[i].Loser.FullName);
                    rowEntries.Add(pickView.FindFighterOddText(exhibition.Fights[i].Loser, exhibition));
                    foreach (Analyst analyst in pickView.EntityFinder.AnalystFinder.GetAllAnalysts())
                    {
                        rowEntries.Add(pickView.FindPickText(analyst, exhibition.Fights[i]));
                    }
                    csvGenerator.AddRow(rowEntries);
                }   
            }
            return File(new UTF8Encoding().GetBytes(csvGenerator.GetContent()), "text/csv", "FightData.csv");
        }

        private PickView GetPickView()
        {
            PickView pickView = new PickView(context);
            pickView.LoadData();
            return pickView;
        }

    }
}