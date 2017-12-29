using FightData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FightData.DataLayer
{

    public class DataUtilities
    {
        private FightPicksContext context;

        public DataUtilities()
        {
            context = new FightPicksContext();
        }

        #region Events
        public List<Event> GetAllEvents()
        {
            return context.Event.ToList();
        }

        public void AddEvent(Event eventObj)
        {
            context.Event.Add(eventObj);
            context.SaveChanges();
        }
        #endregion

        #region Websites
        public List<Website> GetAllWebsites()
        {
            return context.Website.ToList();
        }
        #endregion

        #region Webpages
        public List<Webpage> GetAllWebpages()
        {
            return context.Webpage.Include(w => w.Event).Include(w => w.Website).ToList();
        }

        public void AddWebpage(Webpage webpage)
        {
            context.Webpage.Add(webpage);
            context.SaveChanges();
        }
        #endregion

        #region Fighters
        public void AddFighter(Fighter fighter)
        {
            context.Fighter.Add(fighter);
            context.SaveChanges();
        }

        public Fighter FindFighter(string fighterName)
        {
            fighterName = CleanFighterName(fighterName);
            return context.Fighter.FirstOrDefault(f => f.FullName == fighterName);
        }

        public Fighter PopulateFighterName(string name)
        {
            name = CleanFighterName(name);
            Fighter fighter = new Fighter();
            var names = name.Split(new string[] { " " }, StringSplitOptions.None).ToList();
            fighter.FirstName = names.First();
            fighter.LastName = names.Last();
            fighter.FullName = name;
            names.Remove(names.First());
            names.Remove(names.Last());
            names.ForEach(n =>
            {
                fighter.MiddleName += n;
            });
            return fighter;
        }

        private string CleanFighterName(string name)
        {
            name = name.Replace("(c)", "");
            name = name.Trim();
            return name;
        }
        #endregion

        #region Fights
        public List<Fight> GetAllFights()
        {
            return context.Fight.ToList();
        }

        public void AddFight(Fight fight)
        {
            context.Fight.Add(fight);
            context.SaveChanges();  
        }
        #endregion

    }
}
