using FightData.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    public class TestDataEntity
    {
        private Database database;
        protected FightPicksContext context;
        protected DatabaseDataGenerator databaseDataGenerator;

        public TestDataEntity()
        {
            database = new Database();
            context = database.Context;
            databaseDataGenerator = new DatabaseDataGenerator(context);
        }
    }
}
