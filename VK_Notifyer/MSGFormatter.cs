internal class MSGFormatter
{
    public string Split(string str) {
        if (str.Length > 35) {
            string output = "";
            for (int i = 0; i < 35; i++)
                output += str[i];
            return output + "...";
        }
       return str;
    }
}
