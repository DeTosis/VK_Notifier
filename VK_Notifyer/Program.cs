class Program
{

    static readonly string api_key = "https://api.vk.com";
    static readonly string token = "";

    static getConversations getConversations = new getConversations(api_key, token, "5.199");
    static getUsers getUsers = new getUsers(api_key, token, "5.199");
    static response_Parser parser = new response_Parser(getUsers);

    public static async Task Main() {
        parser.Startup(data: await getConversations._getConversations(0, 0, getConversations.e_filter.unread));

        while (true) {
            dynamic MSG_data = await getConversations._getConversations(0, 0, getConversations.e_filter.unread);
            parser.ParseMSGResponse(MSG_data).GetAwaiter().GetResult();
        }
    }
}