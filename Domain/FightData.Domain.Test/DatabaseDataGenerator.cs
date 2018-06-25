using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    public class DatabaseDataGenerator
    {
        private FightPicksContext context;

        public DatabaseDataGenerator(FightPicksContext context)
        {
            this.context = context;
        }

        public void AddEvent()
        {
            UfcEvent ufcEvent = EntityDataGenerator.UfcEvent();
            context.Add(ufcEvent);
            context.SaveChanges();
        }

        public void AddPick()
        {
            context.Add(EntityDataGenerator.Pick());
            context.SaveChanges();
        }

        public void AddRegularFighter()
        {
            context.Add(EntityDataGenerator.Fighter());
            context.SaveChanges();
        }
    }
}
