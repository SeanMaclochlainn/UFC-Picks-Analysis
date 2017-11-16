using FightData.Models;
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
        private string connectionString;
        public DataUtilities(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection GetDatabaseConnection()
        {
            //string connectionString = Program.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            var conn = new SqlConnection(connectionString);
            //Todo add to web config
            // var conn = new SqlConnection("Server = SEAN-PC; Database=FightPicks; Trusted_Connection=True");
            conn.Open();
            return conn;
        }

        public List<Website> GetAllWebsites()
        {
            List<Website> websites = new List<Website>();

            using (SqlConnection conn = GetDatabaseConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "GetAllWebsites";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var website = new Website();
                        website.Id = (int)rdr["Id"];
                        website.Name = (string)rdr["Name"];
                        websites.Add(website);
                    }
                }
            }
            return websites;
        }

        public void AddWebPage(Webpage webpage)
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("AddWebpage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@URL", webpage.URL));
            cmd.Parameters.Add(new SqlParameter("@WebsiteId", webpage.Website.Id));
            cmd.Parameters.Add(new SqlParameter("@EventId", webpage.UFCEvent.Id));
            cmd.Parameters.Add(new SqlParameter("@Data", webpage.Data));
            cmd.ExecuteNonQuery();
        }

        public int AddEvent(string eventName)
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("AddEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EventName", eventName));
            cmd.Parameters.Add(new SqlParameter("@EventId", SqlDbType.Int));
            cmd.Parameters["@EventId"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            int eventId = (int)cmd.Parameters["@EventId"].Value;
            return eventId;
        }

        public List<Event> GetAllEvents()
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("GetAllEvents", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            List<Event> eventList = new List<Event>();
            while (rdr.Read())
            {
                var eventObj = new Event();
                eventObj.Id = (int)rdr["Id"];
                eventObj.EventName = (string)rdr["EventName"];
                eventList.Add(eventObj);
            }
            return eventList;
        }

        public Webpage GetWebPage(int eventId, int websiteId)
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("GetWebpage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@eventId", eventId));
            cmd.Parameters.Add(new SqlParameter("@websiteId", websiteId));
            var rdr = cmd.ExecuteReader();
            var webpage = new Webpage();
            rdr.Read();
            webpage.Data = (string)rdr["Data"];
            webpage.URL = (string)rdr["URL"];
            return webpage;
        }

        public void AddFight(Fight fight)
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("AddFight", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FighterAId", fight.FighterA.Id));
            cmd.Parameters.Add(new SqlParameter("@FighterBId", fight.FighterB.Id));
            cmd.Parameters.Add(new SqlParameter("@WinnerId", fight.Winner.Id));
            cmd.Parameters.Add(new SqlParameter("@EventId", fight.Event.Id));
            cmd.Parameters.Add(new SqlParameter("@CardTypeId", fight.CardTypeId));
            cmd.ExecuteNonQuery();
        }

        public List<Analyst> GetAllAnalysts()
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("GetAllAnalysts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            List<Analyst> analystList = new List<Analyst>();
            while (rdr.Read())
            {
                var analyst = new Analyst();
                analyst.Id = (int)rdr["Id"];
                analyst.Name = (string)rdr["Name"];
                analystList.Add(analyst);
            }
            return analystList;
        }

        public void AddPick(int analystId, string pick, bool correct, int fightId, int fighterPickId)
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("AddPick", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AnalystId", analystId));
            cmd.Parameters.Add(new SqlParameter("@Pick", pick));
            cmd.Parameters.Add(new SqlParameter("@Correct", correct));
            cmd.Parameters.Add(new SqlParameter("@FightId", fightId));
            cmd.Parameters.Add(new SqlParameter("@FighterPickId", fighterPickId));
            cmd.ExecuteNonQuery();
        }

        public List<Fighter> GetAllFighters()
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("GetAllFighters", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            List<Fighter> fighterList = new List<Fighter>();
            while (rdr.Read())
            {
                var fighter = new Fighter();
                fighter.Id = (int)rdr["Id"];
                fighter.FullName = (string)rdr["FullName"];
                fighter.MiddleName = rdr["MiddleName"] != DBNull.Value ? (string)rdr["MiddleName"] : "";
                fighter.LastName = (string)rdr["LastName"];
                fighterList.Add(fighter);
            }
            return fighterList;
        }

        public List<AltName> GetAltNames()
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("GetAllAltNames", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            List<AltName> altNameList = new List<AltName>();
            while (rdr.Read())
            {
                var altName = new AltName();
                altName.Fighter = new Fighter();
                altName.Name = (string)rdr["Name"];
                altName.Fighter.Id = (int)rdr["FighterId"];
                altNameList.Add(altName);
            }
            return altNameList;
        }

        //todo do this without passing in list of fighters
        public Fighter FindFighter(string name, List<Fighter> allFighters, List<AltName> altNames)
        {
            Fighter fighter = null;
            fighter = allFighters.FirstOrDefault(i => i.FullName == name);
            return fighter;
        }

        public int AddFighter(Fighter fighter)
        {
            var conn = GetDatabaseConnection();
            SqlCommand cmd = new SqlCommand("AddFighter", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@FullName", fighter.FullName));
            cmd.Parameters.Add(new SqlParameter("@FirstName", fighter.FirstName));
            cmd.Parameters.Add(new SqlParameter("@MiddleName", fighter.MiddleName));
            cmd.Parameters.Add(new SqlParameter("@LastName", fighter.LastName));
            cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            int fighterId = (int)cmd.Parameters["@Id"].Value;
            return fighterId;
        }

        public Fighter PopulateFighterName(string name)
        {
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

        public Fighter GetAnalystsFighterPick(Fight fight, string analystPick, List<AltName> altNames)
        {
            Fighter fighterA = fight.FighterA;
            Fighter fighterB = fight.FighterB;
            List<string> fighterANames = new List<string>();
            fighterANames.Add(fighterA.FullName);
            fighterANames.Add(fighterA.LastName);
            fighterANames.AddRange(altNames.Where(i => i.Fighter.Id == fighterA.Id).Select(i => i.Name));


            List<string> fighterBNames = new List<string>();
            fighterBNames.Add(fighterB.FullName);
            fighterBNames.Add(fighterB.LastName);
            fighterBNames.AddRange(altNames.Where(i => i.Fighter.Id == fighterB.Id).Select(i => i.Name));

            bool fighterAPick = false;
            bool fighterBPick = false;
            fighterAPick = (fighterANames.Contains(analystPick));
            fighterBPick = fighterBNames.Contains(analystPick);
            if (fighterAPick && !fighterBPick)
                return fighterA;
            else if (fighterBPick && !fighterAPick)
                return fighterB;
            else
            {
                Console.WriteLine("\nPick: {0}", analystPick);
                Console.WriteLine("Event: {0}", fight.Event.EventName);
                Console.WriteLine("\nFighter A Names:");
                fighterANames.ForEach(fn =>
                {
                    Console.WriteLine(fn);
                });
                Console.WriteLine("\nFighter B Names:");
                fighterBNames.ForEach(fn =>
                {
                    Console.WriteLine(fn);
                });
                bool validInput = false;
                while (!validInput)
                {
                    Console.WriteLine("\nSelect fighter: (A/B)");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "a")
                    {
                        validInput = true;
                        AddAlternateName(fighterA.Id, analystPick);
                        return fighterA;
                    }
                    else if (input.ToLower() == "b")
                    {
                        validInput = true;
                        AddAlternateName(fighterB.Id, analystPick);
                        return fighterB;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                return null;
            }
        }

        public void AddAlternateName(int fighterId, string alternateName)
        {
            using (SqlConnection conn = GetDatabaseConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "AddAltName";
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FighterId", fighterId));
                    cmd.Parameters.Add(new SqlParameter("@AltName", alternateName));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Fight> GetAllfights()
        {
            List<Fight> fights = new List<Fight>();

            using (SqlConnection conn = GetDatabaseConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "GetAllFights";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var fight = new Fight();
                        fight.FighterA = new Fighter();
                        fight.FighterB = new Fighter();
                        fight.Winner = new Fighter();
                        fight.Event = new Event();
                        fight.Id = (int)rdr["Id"];
                        fight.FighterA.Id = (int)rdr["FighterAId"];
                        fight.FighterB.Id = (int)rdr["FighterBId"];
                        fight.Winner.Id = (int)rdr["WinnerId"];
                        fight.Event.Id = (int)rdr["EventId"];
                        fights.Add(fight);
                    }
                }
            }
            return fights;
        }

        public List<Pick> GetAllPicks()
        {
            List<Pick> picks = new List<Pick>();

            using (SqlConnection conn = GetDatabaseConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "GetAllPicks";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var pick = new Pick();
                        pick.Id = (int)rdr["Id"];
                        pick.AnalystId = (int)rdr["AnalystId"];
                        pick.Correct = (bool)rdr["Correct"];
                        pick.FightId = (int)rdr["FightId"];
                        pick.FighterPickId = (int)rdr["FighterPickId"];
                        picks.Add(pick);
                    }
                }
            }
            return picks;
        }
    }
}
