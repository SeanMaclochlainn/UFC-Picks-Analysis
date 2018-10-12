using System.Net;

namespace FightData.Domain.Updaters
{
    public class ConnectedClient : Client
    {
        public string Download(string url)
        {
            string result = "";
            using (WebClient client = new WebClient())
            {
                result = client.DownloadString(url);
            }
            return result;
        }
    }
}
