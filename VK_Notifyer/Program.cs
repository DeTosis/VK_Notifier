using VK_Notifyer;

class Program
{

    static readonly string api_key = "https://api.vk.com";
    static readonly string token = "";

    static readonly GetConversations getConversations = new(api_key, token, "5.199");
    static readonly GetUsers getUsers = new(api_key, token, "5.199");
    static readonly ResponseParser parser = new(getUsers);

    public static async Task Main() {
        parser.Startup(await getConversations._get(GetConversations.e_filter.unread));

        while (true) {
            dynamic MSG_data = await getConversations._get(GetConversations.e_filter.unread);
            parser.ParseMSGResponse(MSG_data).GetAwaiter().GetResult();
        }
    }
}