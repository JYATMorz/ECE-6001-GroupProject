namespace GameFellowship.Data
{
    public class Conversation
    {
        public string UserName { get; set; } = "Undefined User";
        public DateTime SendTime { get; set; } = DateTime.MinValue;
        public string Context { get; set; } = string.Empty;

        public Conversation() { }

        public Conversation(string name, DateTime time, string context)
        {
            UserName = name;
            SendTime = time;
            Context = context;
        }
    }
}