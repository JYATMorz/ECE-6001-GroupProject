namespace GameFellowship.Data
{
    public class PostInfo
    {
        public int PostId { get; init; } = -1;
        public DateTime LastUpdate { get; init; }

        public string GameName { get; set; } = "Empty Game Name";
        public string MatchType { get; set; } = "Empty Game Type";

        public List<string> Requirements { get; set; } = new();
        public string Description { get; set; } = string.Empty;

        public int TotalPeople { get; set; } = 0;
        public int CurrentPeople { get; set; } = 0;

        public bool PlayNow { get; set; } = true;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool AudioChat { get; set; } = false;
        public string AudioPlatform { get; set; } = "Any Platform";
        public string AudioLink { get; set; } = string.Empty;

        public PostInfo() { }

        public PostInfo(int id, DateTime update, string name, string match, List<string> requirements,
            string? description, int total, int current, bool now = true, DateTime? start = null, DateTime? end = null,
            bool audio = false, string? platform = null, string? link = null)
        {
            PostId = id;
            update = LastUpdate;
            GameName = name;
            MatchType = match;
            Requirements = requirements;
            TotalPeople = total;
            CurrentPeople = current;

            if (description is not null) Description = description;

            if (start is null || end is null)
            {
                PlayNow = true;
            }
            else
            {
                PlayNow = false;
                StartDate = start;
                EndDate = end;
            }

            if (platform is null || link is null)
            {
                AudioChat = false;
            }
            else
            {
                AudioChat = true;
                AudioPlatform = platform;
                AudioLink = link;
            }
        }

    }
}