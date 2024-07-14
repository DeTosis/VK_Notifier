public static class APIBase
{
    public static (string, string) FormatBaseAPI(string api, string token, string api_v) {
        string linq_s = $"{api}/method/";
        string linq_e = $"&access_token={token}&v={api_v}";

        return (linq_s, linq_e);
    }
}