using System.Net;

namespace AdventOfCode2015.Services
{
    public class WebReader : IWebReader
    {
        public string Content(string URL)
        {
            WebClient client = new WebClient();
            return client.DownloadString(URL);
        }
    }
}