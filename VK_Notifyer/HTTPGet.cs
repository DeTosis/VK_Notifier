using Newtonsoft.Json.Linq;

namespace VK_Notifyer
{
    internal static class HTTPGet
    {
        public static async Task<dynamic> _get(string linq) {
            HttpClient client = new();

            using HttpResponseMessage response = await client.GetAsync(linq);
            string responseBody = await response.Content.ReadAsStringAsync();

            dynamic data = JObject.Parse(responseBody);
            return data;
        }
    }
}
