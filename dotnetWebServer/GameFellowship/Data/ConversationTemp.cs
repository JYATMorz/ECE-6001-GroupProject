namespace GameFellowship.Data;

public class ConversationTemp
{
    public int UserID { get; set; } = -1;
    public DateTime SendTime { get; set; } = DateTime.MinValue;
    public string Context { get; set; } = string.Empty;

    public ConversationTemp() { }

    public ConversationTemp(int userId, string context, DateTime? time = null)
    {
        UserID = userId;
        SendTime = time ?? DateTime.Now;
        Context = context;
    }
}