using FightData.Models.DataModels;
using FightData.Models.ViewModels;
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
            return context.Event
                .Include("Fights.Loser")
                .Include("Fights.Winner")
                .Include("Fights.Picks")
                .Include("Webpages.Website")
                .ToList();
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

        public void AddAltFighterName(string name, Fighter fighter)
        {
            context.FighterAltName.Add(new FighterAltName() { Name = name, Fighter = fighter });
            context.SaveChanges();
        }

        ///<summary>
        ///Searches for fighter in database
        ///</summary>
        public Fighter FindFighter(string fighterName)
        {
            fighterName = CleanFighterName(fighterName);
            return context.Fighter.FirstOrDefault(f => f.FullName == fighterName);
        }

        ///<summary>
        ///Searches for fighter within event
        ///</summary>
        public Fighter FindFighter(string fighterName, Event eventObj)
        {
            List<Fighter> fighters = eventObj.Fights.Select(f => f.Winner).ToList();
            fighters.AddRange(eventObj.Fights.Select(f => f.Loser).ToList());
            Fighter fighter = fighters.FirstOrDefault(f => f.FullName == fighterName || f.LastName == fighterName);
            return fighter;
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

        ///<summary>
        ///Searches for a fight within an event by using one of the fighters
        ///</summary>
        public Fight FindFight(Fighter fighter, Event eventObj)
        {
            return eventObj.Fights.FirstOrDefault(f => f.Winner == fighter || f.Loser == fighter);
        }

        public void AddFight(Fight fight)
        {
            context.Fight.Add(fight);
            context.SaveChanges();
        }
        #endregion

        #region CardTypes
        public CardType GetCardType(string cardName)
        {
            return context.CardType.FirstOrDefault(ct => ct.Name == cardName);
        }
        #endregion

        #region Analysts
        public List<Analyst> GetAllAnalysts()
        {
            return context.Analyst.ToList();
        }

        public Analyst AddAnalyst(string name)
        {
            Analyst analyst = new Analyst()
            {
                Name = name
            };
            context.Analyst.Add(analyst);
            context.SaveChanges();
            return analyst;
        }

        public void AddAnalystAltName(string name, Analyst analyst)
        {
            AnalystAltName analystAltName = new AnalystAltName()
            {
                Name = name,
                Analyst = analyst
            };
            context.AnalystAltName.Add(analystAltName);
            context.SaveChanges();
        }

        public Analyst GetAnalyst(string name)
        {
            return context.Analyst.FirstOrDefault(a => a.Name == name);
        }
        #endregion

        #region Picks
        public void AddPick(Pick pick)
        {
            context.Pick.Add(pick);
            context.SaveChanges();
        }
        #endregion

        #region ViewModels
        public FightEventVM GetFightEventVM()
        {
            List<Event> events = GetAllEvents();
            List<Analyst> analysts = GetAllAnalysts();
            FightEventVM fightEventVM = new FightEventVM()
            {
                Analysts = analysts,
                FightEvents = events
            };
            return fightEventVM;
        }
        #endregion

    }
}
