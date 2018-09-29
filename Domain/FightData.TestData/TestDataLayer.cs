using FightData.Domain.Entities;
using FightData.TestData;
using FightData.TestData.EntityGenerators;

namespace FightData.Domain.Test
{
    public class TestDataLayer
    {
        protected FightPicksContext context;
        protected EntityGenerator entityGenerator;

        public TestDataLayer()
        {
            context = new TestDatabase().Context;
            entityGenerator = new EntityGenerator(context);
            AddWebsites();
        }

        private void AddWebsites()
        {
            new Website(context) { Id = 1, WebsiteName = WebsiteName.Wikipedia, WebsiteType = WebsiteType.Result }.Add();
            new Website(context) { Id = 2, WebsiteName = WebsiteName.MMAJunkie, WebsiteType = WebsiteType.Pick }.Add();
        }
    }
}
