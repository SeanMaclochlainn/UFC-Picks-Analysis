using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    public class TestDomain
    {
        private Database database;
        protected FightPicksContext context;
        protected DatabaseDataAdder databaseDataGenerator;
        protected EntityDataGenerator entityDataGenerator;

        public TestDomain()
        {
            database = new Database();
            context = database.Context;
            databaseDataGenerator = new DatabaseDataAdder(context);
            entityDataGenerator = new EntityDataGenerator(context);
        }
    }
}
