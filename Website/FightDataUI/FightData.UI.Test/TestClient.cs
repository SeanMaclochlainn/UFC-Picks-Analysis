using FightDataUI.ViewModels;

namespace FightData.UI.Test
{
    public class TestClient : Client
    {
        public string Download(string url)
        {
            return "downloadedstring";
        }
    }
}
