using System.Net;

namespace FightData.Domain.EntityCreation
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
