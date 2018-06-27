﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FightData.Domain.Entities;

namespace FightData.Domain
{
    public class FighterFinder : DataFinder
    {
        private string name;

        public FighterFinder(string name) : this(name, new FightPicksContext()) { }

        public FighterFinder(string name, FightPicksContext context) : base(context)
        {
            this.name = name;
            Fighter = FindFighter();
            FighterExists = DoesFighterExist();
        }

        public bool FighterExists { get; private set; }
        
        public Fighter Fighter { get; private set; }

        private Fighter FindFighter()
        {
            return context.Fighters.FirstOrDefault(f => f.FullName == name);
        }

        private bool DoesFighterExist()
        {
            return !(Fighter == null);
        }
    }
}
