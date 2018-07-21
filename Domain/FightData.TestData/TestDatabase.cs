using FightData.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FightData.TestData
{
    public class TestDatabase
    {
        private SqliteConnection connection;
        private DbContextOptions<FightPicksContext> options;

        public TestDatabase()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<FightPicksContext>()
                .UseSqlite(connection)
                .Options;

            Context = new FightPicksContext(options);

            Context.Database.EnsureCreated();
        }


        public FightPicksContext Context { get; private set; }
    }
}
