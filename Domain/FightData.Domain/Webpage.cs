﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain
{
    public class Webpage
    {
        private FightPicksContext context;

        public Webpage() : this(new FightPicksContext()) { }

        public Webpage(FightPicksContext context)
        {
            this.context = context;
        }

        public Webpage(string Url, Website website, string data)
        {
            this.Url = Url;
            this.Website = website;
            this.Data = data;
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public Website Website { get; set; }
        public UfcEvent Event { get; set; }
        public string Data { get; set; }

        //public void UpdateWebpage(string data, string url)
        //{
        //    this.Data = data;
        //    this.Url = url;
        //    context.SaveChanges();
        //}

        //public void AddWebpage()
        //{
        //    context.Webpages.Add(this);
        //    context.SaveChanges();
        //}
    }
}
