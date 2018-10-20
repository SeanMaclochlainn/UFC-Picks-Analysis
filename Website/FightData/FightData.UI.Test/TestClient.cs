using FightData.Domain.Updaters;

namespace FightData.UI.Test
{
    public class TestClient : Client
    {
        private string downloadedData;

        public TestClient()
        {
            downloadedData = "downloadedstring";
        }

        public TestClient(string downloadedData)
        {
            this.downloadedData = downloadedData;
        }

        public string Download(string url)
        {
            return downloadedData;
        }
    }
}
