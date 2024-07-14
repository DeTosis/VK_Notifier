using VK_Notifyer;

internal class ResponseParser
{
    private static List<string> notifiedMessages = new();
    private static List<(dynamic, dynamic, string)> notificationQueue = new();
    private GetUsers _getUsers;
    private NotificationHandler notification;

    public ResponseParser(GetUsers getUsers) => _getUsers = getUsers;

    public void Startup(dynamic data) {
        for (int i = 0; i < data.response["items"].Count; i++) {
            string msg_id = data.response["items"][i]["conversation"]["last_message_id"].ToString();
            if (!notifiedMessages.Contains(msg_id)) {
                if (!string.IsNullOrEmpty(msg_id)) notifiedMessages.Add(msg_id);
            } else continue;
        }
    }

    public async Task ParseMSGResponse(dynamic data) {
        for (int i = 0; i < data.response["items"].Count; i++) {
            try {
                if (data.response["items"][i]["conversation"]["push_settings"]["no_sound"].ToString() == "true") continue;
            } catch {
                string msg_id = data.response["items"][i]["conversation"]["last_message_id"].ToString();
                if (!notifiedMessages.Contains(msg_id)) {
                    if (!string.IsNullOrEmpty(msg_id)) notifiedMessages.Add(msg_id);
                    string msg_text = data.response["items"][i]["last_message"]["text"].ToString();
                    MSGFormatter formatter = new();
                    msg_text = formatter.Split(msg_text);
                    string sender_id = data.response["items"][i]["last_message"]["from_id"].ToString();
                    dynamic senderData = await _getUsers._get(Convert.ToInt32(sender_id));
                    var senderData_p = ParseSenderData(senderData);

                    var output = (senderData_p.Item1, senderData_p.Item2, msg_text);
                    notificationQueue.Add(output);
                } else continue;
            }
        }
        Notify();
    }

    private void Notify() {
        foreach (var output in notificationQueue) {
            notification = new NotificationHandler(output);
        }
        notificationQueue.Clear();
    }

    private (string, string) ParseSenderData(dynamic data) {
        string lastName = data.response[0]["last_name"].ToString();
        string firstName = data.response[0]["first_name"].ToString();

        return (firstName, lastName);
    }
}

