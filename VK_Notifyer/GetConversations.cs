using Newtonsoft.Json.Linq;
using VK_Notifyer;


public class GetConversations {
    public enum e_filter { all, important, unanswered, unread, archive };
    readonly string method_Prefix = "messages.getConversations";

    private static (string,string) _api;
    public GetConversations(string api, string token, string api_v) {
        _api = APIBase.FormatBaseAPI(api, token, api_v);
    }

    public async Task<dynamic> _get(e_filter filter) {
        string _filter = $"filter={filter.ToString()}";
        string APILinq = $"{_api.Item1}{method_Prefix}?{_filter}&{_api.Item2}";

        return await HTTPGet._get(APILinq);
    }
}