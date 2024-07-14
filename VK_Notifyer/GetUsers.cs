using Newtonsoft.Json.Linq;

namespace VK_Notifyer
{
    public class GetUsers
    {
        static readonly string method_Prefix = "users.get";

        static (string, string) _api;
        public GetUsers(string api, string token, string api_v) {
            _api = APIBase.FormatBaseAPI(api, token, api_v);
        }

        public async Task<dynamic> _get(int user_ids) {
            string _user_ids = $"user_ids={user_ids}";
            string APILinq = $"{_api.Item1}{method_Prefix}?{_user_ids}&{_api.Item2}";

            return await HTTPGet._get(APILinq);
        }
    }
}
