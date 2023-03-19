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
        public int[] CurrentIDs { get; set; } = { };

        public bool PlayNow { get; set; } = true;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        public bool AudioChat { get; set; } = false;
        public string AudioPlatform { get; set; } = "No Platform";
        public string AudioLink { get; set; } = string.Empty;

        public PostInfo() { }

        public PostInfo(int id, DateTime update, string name, string match, List<string> requirements,
            string? description, int total, int current, int[] userIds, DateTime? start = null, DateTime? end = null,
            string? platform = null, string? link = null)
        {
            PostId = id;
            LastUpdate = update;
            GameName = name;
            MatchType = match;
            Requirements = requirements;
            TotalPeople = total;
            CurrentPeople = current;
            CurrentIDs = userIds;

            if (description is not null) Description = description;

            if (start is null || end is null)
            {
                PlayNow = true;
            }
            else
            {
                PlayNow = false;
                StartDate = (DateTime)start;
                EndDate = (DateTime)end;
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