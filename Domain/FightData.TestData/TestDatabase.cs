using FightData.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace FightData.TestData
{
    public class TestDatabase
    {
        private SqliteConnection connection;
        private DbContextOptions<FightPicksContext> options;
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        public TestDatabase()
        {

            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<FightPicksContext>()
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlite(connection)
                .Options;

            Context = new FightPicksContext(options);

            Context.Database.EnsureCreated();
        }


        public FightPicksContext Context { get; private set; }
    }
}
