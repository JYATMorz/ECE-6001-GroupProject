namespace GameFellowship.Data;

public class Conversation
{
    public int UserID { get; set; } = -1;
    public DateTime SendTime { get; set; } = DateTime.MinValue;
    public string Context { get; set; } = string.Empty;

    public Conversation() { }

    public Conversation(int userId, string context, DateTime? time = null)
    {
        UserID = userId;
        SendTime = time ?? DateTime.Now;
        Context = context;
    }
}