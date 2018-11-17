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
                client.Headers.Add("user-agent", " Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
                result = client.DownloadString(url);
            }
            return result;
        }
    }
}
