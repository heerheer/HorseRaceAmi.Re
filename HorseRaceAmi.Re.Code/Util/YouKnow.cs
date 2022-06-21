using System.Text.Json;
using System.Text.Json.Nodes;

namespace HorseRaceAmi.Re.Code.Util
{
    public class YouKnow
    {
        public static string Get()
        {
            return "你知道吗维护中";
            using (var h = new HttpClient())
            {
                var x = JsonSerializer.Deserialize<JsonNode>(h
                    .GetAsync("http://slapi.heerdev.top/youknow/random/%E9%BB%98%E8%AE%A4").Result.Content
                    .ReadAsStringAsync().Result);
                return x["text"].ToString();
            }
        }
    }
}