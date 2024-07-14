using System.Windows.Controls;
using VK_Notifyer;

class Program
{

    static readonly string api_key = "https://api.vk.com";
    static readonly string token = "";

    static GetConversations getConversations = new(api_key, token, "5.199");
    static GetUsers getUsers = new(api_key, token, "5.199");
    static ResponseParser parser = new(getUsers);

    public static async Task Main() {
        Tray();
        parser.Startup(await getConversations._get(GetConversations.e_filter.unread));

        while (true) {
            dynamic MSG_data = await getConversations._get(GetConversations.e_filter.unread);
            parser.ParseMSGResponse(MSG_data).GetAwaiter().GetResult();
        }
    }

    public static void Tray() {
        Thread thread = new Thread(() => {
            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Text = "Vk Notifier";

            string workingDirectory = Environment.CurrentDirectory;
            trayIcon.Icon = new Icon($"{workingDirectory}\\Resources\\vkIco.ico");

            ContextMenu trayMenu = new ContextMenu();

            trayIcon.Visible = true;
            Application.Run();
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }
}