using Newtonsoft.Json.Linq;

internal class APIBase
{
    public string api;
    public string token;
    public string api_v;

    public APIBase(string api, string token, string api_v) {
        this.api = api;
        this.token = token;
        this.api_v = api_v;
    }
    public (string, string) FormatBaseAPI() {
        string linq_s = $"{api}/method/";
        string linq_e = $"&access_token={token}&v={api_v}";

        return (linq_s, linq_e);
    }
}

public class getConversations
{
    private static HttpClient client = new HttpClient();

    static APIBase baseAPI;
    static readonly string method_Prefix = "messages.getConversations";
    public getConversations(string api, string token, string api_v) {
        baseAPI = new APIBase(api, token, api_v);
    }

    public enum e_filter { all, important, unanswered, unread, archive };
    public async Task<dynamic> _getConversations(int offset, int count, e_filter filter) {
        string _offset = $"offset={offset}";
        string _count = $"count={count}";
        string _filter = $"filter={filter.ToString()}";

        var api = baseAPI.FormatBaseAPI();
        string APILinq = "";

        if (count == 0)
            APILinq = $"{api.Item1}{method_Prefix}?{_offset}&{_filter}&{api.Item2}";
        else
            APILinq = $"{api.Item1}{method_Prefix}?{_offset}&{_count}&{_filter}&{api.Item2}";

        return await _get(APILinq);
    }

    private async Task<dynamic> _get(string linq) {
        using HttpResponseMessage response = await client.GetAsync(linq);
        string responseBody = await response.Content.ReadAsStringAsync();

        dynamic data = JObject.Parse(responseBody);
        return data;
    }
}

public class getUsers
{
    private static HttpClient client = new HttpClient();

    static APIBase baseAPI;
    static readonly string method_Prefix = "users.get";
    public getUsers(string api, string token, string api_v) {
        baseAPI = new APIBase(api, token, api_v);
    }

    public async Task<dynamic> _getUsers(int user_ids) {
        string _user_ids = $"user_ids={user_ids}";

        var api = baseAPI.FormatBaseAPI();
        string APILinq = $"{api.Item1}{method_Prefix}?{_user_ids}&{api.Item2}";

        return await _get(APILinq);
    }

    public async Task<dynamic> _get(string linq) {
        using HttpResponseMessage response = await client.GetAsync(linq);
        string responseBody = await response.Content.ReadAsStringAsync();

        dynamic data = JObject.Parse(responseBody);
        return data;
    }

}