using System.Text.Json;

namespace Twileloop.Helpers
{

    public class IPDetails
    {
        public string status { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string timezone { get; set; }
        public string isp { get; set; }
        public string org { get; set; }
        public string _as { get; set; }
        public string query { get; set; }
    }

    public static class IPTracker
    {
        public static async Task<IPDetails> GetIPInfoAsync(string ipAddress)
        {
            string apiUrl = $"http://ip-api.com/json/{ipAddress}";
            string jsonResult = await new HttpClient().GetStringAsync(apiUrl);
            var ipInfo = JsonSerializer.Deserialize<IPDetails>(jsonResult);
            return ipInfo;
        }
    }
}
