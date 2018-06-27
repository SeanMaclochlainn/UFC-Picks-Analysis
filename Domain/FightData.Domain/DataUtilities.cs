using FightData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FightData.Domain
{

    public class DataUtilities
    {
        //private FightPicksContext context;

        //public DataUtilities()
        //{
        //    context = new FightPicksContext();
        //}

        //public DataUtilities(DbContextOptions<FightPicksContext> options)
        //{
        //    context = new FightPicksContext(options);
        //}

        //#region Events
        //public List<UfcEvent> GetAllEvents()
        //{
        //    context.Fights.Load();
        //    context.Fighters.Load();
        //    context.FighterAltNames.Load();
        //    context.Picks.Load();
        //    context.Webpages.Load();
        //    context.Websites.Load();
        //    return context.Events.ToList();
        //}

        //public void AddEvent(UfcEvent eventObj)
        //{
        //    context.Events.Add(eventObj);
        //    context.SaveChanges();
        //}

        //public UfcEvent GetEvent(int id)
        //{
        //    return GetAllEvents().Single(e => e.Id == id);
        //}

        //public UfcEvent RefreshEvent(UfcEvent eventObj)
        //{
        //    return GetEvent(eventObj.Id);
        //}

        //public List<Fighter> GetFighters(UfcEvent eventObj)
        //{
        //    List<Fighter> fighters = eventObj.Fights.Select(f => f.Winner).ToList();
        //    fighters.AddRange(eventObj.Fights.Select(f => f.Loser).ToList());
        //    return fighters;
        //}

        //public void RemoveMatchingLastNameFightersPicks(UfcEvent eventObj)
        //{
        //    var list = context.Picks.Where(p => p.Fight.Winner.LastName == "Silva");
        //    foreach (Fight fight in eventObj.Fights)
        //    {
        //        foreach (Fighter fighter in fight.GetAllFighters())
        //        {
        //            if (Fighter.IsFighterInList(eventObj.FightersWithMatchingLastNames, fighter))
        //            {
        //                DeletePicks(fight.Picks);
        //            }
        //        }
        //    }
        //}
        //#endregion

        //#region Websites
        //public List<Website> GetAllWebsites()
        //{
        //    return context.Websites.ToList();
        //}

        //public Website GetWebsite(int id)
        //{
        //    return context.Websites.FirstOrDefault(w => w.Id == id);
        //}
        //#endregion

        //#region Webpages
        //public List<Webpage> GetAllWebpages()
        //{
        //    return context.Webpages.Include(w => w.Event).Include(w => w.Website).ToList();
        //}

        //public void AddWebpage(Webpage webpage)
        //{
        //    context.Webpages.Add(webpage);
        //    context.SaveChanges();
        //}

        //public Webpage FindWebpage(int eventId, int websiteId)
        //{
        //    List<Webpage> webpages = GetAllWebpages();
        //    Webpage webpage = webpages.FirstOrDefault(w =>
        //    w.Website.Id == websiteId &&
        //    w.Event.Id == eventId);
        //    return webpage;
        //}

        //public Webpage UpdateWebpage(Webpage webpage)
        //{
        //    Webpage existingWebpage = FindWebpage(webpage.Event.Id, webpage.Website.Id);
        //    existingWebpage.Url = webpage.Url;
        //    existingWebpage.Data = webpage.Data;
        //    context.SaveChanges();
        //    return existingWebpage;
        //}

        //#endregion

        //#region Fighters
        //public bool IsLastNameDuplicated(string lastName, List<Fighter> fighters)
        //{
        //    int fighterSameNames = fighters.Count(f => f.LastName == lastName);
        //    if (fighterSameNames > 1)
        //        return true;
        //    else
        //        return false;
        //}

        //public List<Fighter> GetFightersWithMatchingLastName(string lastName, List<Fighter> fighters)
        //{
        //    List<Fighter> fightersWithMatchingLastName = new List<Fighter>();
        //    fightersWithMatchingLastName.AddRange(fighters.Where(f => f.LastName == lastName).ToList());
        //    return fightersWithMatchingLastName;
        //}

        //public void AddFighter(Fighter fighter)
        //{
        //    context.Fighters.Add(fighter);
        //    context.SaveChanges();
        //}

        //public void AddAltFighterName(string name, Fighter fighter)
        //{
        //    context.FighterAltNames.Add(new FighterAltName() { Name = name, Fighter = fighter });
        //    context.SaveChanges();
        //}

        //public Fighter FindFighter(string fighterName, UfcEvent eventObj)
        //{
        //    List<Fighter> fighters = GetFighters(eventObj);
        //    return FindFighter(fighterName, fighters);
        //}

        //public Fighter FindFighter(string fighterName, List<Fighter> fighters)
        //{
        //    fighterName = CleanFighterName(fighterName);
        //    List<FighterAltName> fighterAltNames = new List<FighterAltName>();

        //    fighters.ForEach(f => fighterAltNames.AddRange(f.FighterAltNames));

        //    Fighter fighter = fighters.SingleOrDefault(f => f.FullName == fighterName);
        //    if (fighter == null)
        //    {
        //        fighter = fighters.SingleOrDefault(f => f.LastName == fighterName);
        //    }
        //    if (fighter == null)
        //    {
        //        FighterAltName fighterAltName = fighterAltNames.SingleOrDefault(fan => fan.Name == fighterName);
        //        fighter = fighterAltName != null ? fighterAltName.Fighter : null;
        //    }
        //    return fighter;
        //}

        //public Fighter PopulateFighterName(string name)
        //{
        //    name = CleanFighterName(name);
        //    Fighter fighter = new Fighter();
        //    List<string> names = name.Split(new string[] { " " }, StringSplitOptions.None).ToList();
        //    fighter.FirstName = names.First();
        //    fighter.LastName = names.Last();
        //    fighter.FullName = name;
        //    names.Remove(names.First());
        //    names.Remove(names.Last());
        //    names.ForEach(n =>
        //    {
        //        fighter.MiddleName += n;
        //    });
        //    return fighter;
        //}

        //private string CleanFighterName(string name)
        //{
        //    name = name.Replace("(c)", "");
        //    name = name.Trim();
        //    return name;
        //}

        //public List<Fighter> GetAllFighters()
        //{
        //    return context.Fighters.Include("FighterAltNames").ToList();
        //}
        //#endregion

        //#region Fights
        //public List<Fight> GetAllFights()
        //{
        //    return context.Fights.ToList();
        //}

        /////<summary>
        /////Searches for a fight within an event by using one of the fighters
        /////</summary>
        //public Fight FindFight(Fighter fighter, UfcEvent eventObj)
        //{
        //    return eventObj.Fights.FirstOrDefault(f => f.Winner.FullName == fighter.FullName || f.Loser.FullName == fighter.FullName);
        //}

        //public void AddFight(Fight fight)
        //{
        //    context.Fights.Add(fight);
        //    context.SaveChanges();
        //}
        //#endregion

        //#region CardTypes
        //public CardType GetCardType(string cardName)
        //{
        //    return context.CardTypes.FirstOrDefault(ct => ct.Name == cardName);
        //}
        //#endregion

        //#region Analysts
        //public List<Analyst> GetAllAnalysts()
        //{
        //    return context.Analysts
        //        .Include("AltNames")
        //        .ToList();
        //}

        //public Analyst AddAnalyst(string name, int websiteId)
        //{
        //    Website website = GetWebsite(websiteId);
        //    Analyst analyst = new Analyst()
        //    {
        //        Name = name, 
        //        Website = website
        //    };
        //    context.Analysts.Add(analyst);
        //    context.SaveChanges();
        //    return analyst;
        //}

        //public void AddAnalystAltName(string name, Analyst analyst)
        //{
        //    AnalystAltName analystAltName = new AnalystAltName()
        //    {
        //        Name = name,
        //        Analyst = analyst
        //    };
        //    context.AnalystAltNames.Add(analystAltName);
        //    context.SaveChanges();
        //}

        //public Analyst FindAnalyst(string name)
        //{
        //    List<Analyst> analysts = GetAllAnalysts();
        //    Analyst analyst = analysts.FirstOrDefault(a => a.Name == name);
        //    if (analyst == null)
        //    {
        //        List<AnalystAltName> altNames = new List<AnalystAltName>();
        //        analysts.ForEach(a => altNames.AddRange(a.AltNames));
        //        AnalystAltName altName = altNames.FirstOrDefault(an => an.Name == name);
        //        if (altName != null)
        //            analyst = altName.Analyst;
        //    }
        //    return analyst;
        //}
        //#endregion

        //#region Picks
        //public void AddPick(Pick pick)
        //{
        //    int existingPicks = GetAllPicks()
        //        .Count(p =>
        //        p.Analyst.Id == pick.Analyst.Id &&
        //        p.Fight.Id == pick.Fight.Id);
        //    if (existingPicks == 0)
        //    {
        //        context.Picks.Add(pick);
        //        context.SaveChanges();
        //    }
        //}

        //public List<Pick> GetAllPicks()
        //{
        //    return context.Picks
        //        .Include("Analyst")
        //        .Include("Fight")
        //        .ToList();
        //}

        //public void DeleteAllPicks()
        //{
        //    context.Picks.RemoveRange(context.Picks.ToList());
        //    context.SaveChanges();
        //}

        //public void DeletePicks(List<Pick> picks)
        //{
        //    context.Picks.RemoveRange(picks);
        //    context.SaveChanges();
        //}
        //#endregion

        //#region ViewModels
        ////public FightEventVM GetFightEventVM()
        ////{
        ////    List<UfcEvent> events = GetAllEvents();
        ////    List<Analyst> analysts = GetAllAnalysts();
        ////    FightEventVM fightEventVM = new FightEventVM()
        ////    {
        ////        Analysts = analysts,
        ////        FightEvents = events
        ////    };
        ////    return fightEventVM;
        ////}
        //#endregion

    }
}
