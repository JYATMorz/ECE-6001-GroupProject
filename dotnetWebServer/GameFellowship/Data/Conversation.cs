namespace GameFellowship.Data
{
    public class Conversation
    {
        public int UserID { get; set; } = -1;
        public DateTime SendTime { get; set; } = DateTime.MinValue;
        public string Context { get; set; } = string.Empty;

        public Conversation() { }

        public Conversation(int id, DateTime time, string context)
        {
            UserID = id;
            SendTime = time;
            Context = context;
        }
    }
}