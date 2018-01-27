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
                .Include("Fights")
                .Include("Fights.Loser.FighterAltNames")
                .Include("Fights.Winner.FighterAltNames")
                .Include("Fights.Picks")
                .Include("Webpages.Website")
                .ToList();
        }

        public void AddEvent(Event eventObj)
        {
            context.Event.Add(eventObj);
            context.SaveChanges();
        }

        public List<Fighter> GetFighters(Event eventObj)
        {
            List<Fighter> fighters = eventObj.Fights.Select(f => f.Winner).ToList();
            fighters.AddRange(eventObj.Fights.Select(f => f.Loser).ToList());
            return fighters;
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

        public Webpage FindWebpage(int eventId, int websiteId)
        {
            List<Webpage> webpages = GetAllWebpages();
            Webpage webpage = webpages.FirstOrDefault(w =>
            w.Website.Id == websiteId &&
            w.Event.Id == eventId);
            return webpage;
        }

        public Webpage UpdateWebpage(Webpage webpage)
        {
            Webpage existingWebpage = FindWebpage(webpage.Event.Id, webpage.Website.Id);
            existingWebpage.Url = webpage.Url;
            existingWebpage.Data = webpage.Data;
            context.SaveChanges();
            return existingWebpage;
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

        public Fighter FindFighter(string fighterName, Event eventObj)
        {
            List<Fighter> fighters = GetFighters(eventObj);
            return FindFighter(fighterName, fighters);
        }

        public Fighter FindFighter(string fighterName, List<Fighter> fighters)
        {
            fighterName = CleanFighterName(fighterName);
            List<FighterAltName> fighterAltNames = new List<FighterAltName>();

            fighters.ForEach(f => fighterAltNames.AddRange(f.FighterAltNames));

            Fighter fighter = fighters.SingleOrDefault(f => f.FullName == fighterName);
            if (fighter == null)
            {
                fighter = fighters.SingleOrDefault(f => f.LastName == fighterName);
            }
            if (fighter == null)
            {
                FighterAltName fighterAltName = fighterAltNames.SingleOrDefault(fan => fan.Name == fighterName);
                fighter = fighterAltName != null ? fighterAltName.Fighter : null;
            }
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

        public List<Fighter> GetAllFighters()
        {
            return context.Fighter.Include("FighterAltNames").ToList();
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
            return context.Analyst
                .Include("AltNames")
                .ToList();
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

        public Analyst FindAnalyst(string name)
        {
            List<Analyst> analysts = GetAllAnalysts();
            Analyst analyst = analysts.FirstOrDefault(a => a.Name == name);
            if (analyst == null)
            {
                List<AnalystAltName> altNames = new List<AnalystAltName>();
                analysts.ForEach(a => altNames.AddRange(a.AltNames));
                AnalystAltName altName = altNames.FirstOrDefault(an => an.Name == name);
                analyst = altName.Analyst;
            }
            return analyst;
        }
        #endregion

        #region Picks
        public void AddPick(Pick pick)
        {
            List<Pick> existingPicks = GetAllPicks()
                .Where(p =>
                p.Analyst.Id == pick.Analyst.Id &&
                p.Fight.Id == pick.Fight.Id)
                .ToList();
            if (existingPicks.Count() == 0)
            {
                context.Pick.Add(pick);
                context.SaveChanges();
            }
        }

        public List<Pick> GetAllPicks()
        {
            return context.Pick
                .Include("Analyst")
                .Include("Fight")
                .ToList();
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
