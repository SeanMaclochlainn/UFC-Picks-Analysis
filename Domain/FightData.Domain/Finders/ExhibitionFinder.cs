﻿using System.Collections.Generic;
using System.Linq;
using FightData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FightData.Domain.Finders
{
    public class ExhibitionFinder : DataFinder
    {
        public ExhibitionFinder(FightPicksContext context) : base(context) { }

        public List<Exhibition> FindAllExhibitions()
        {
            return context.Exhibitions.Include(e=>e.Webpages).ToList();
        }
    }
}