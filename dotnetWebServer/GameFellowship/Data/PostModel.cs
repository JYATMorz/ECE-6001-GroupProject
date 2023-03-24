using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data
{
    public class PostModel
    {
        [PostGameValidator(10, "名")]
        public string? GameName { get; set; }

        [PostGameValidator(10, "类型")]
        public string? MatchType { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "队伍要求请少于等于5个捏")]
        [MinLength(1, ErrorMessage = "至少提1个队伍要求吧")]
        public string[] Requirements { get; set; } = { };

        [StringLength(50, ErrorMessage = "描述请精简地少于50个字")]
        public string? Description { get; set; }

        [Required]
        [PostPeopleValidator(1, 100)]
        public int TotalPeople { get; set; }
        [Required]
        [PostPeopleValidator(1, 100)]
        public int CurrentPeople { get; set; }

        [Required]
        public bool PlayNow { get; set; } = false;
        [PostDateValidator(30, true)]
        public DateTime? StartDate { get; set; }
        [PostDateValidator(30, false)]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool AudioChat { get; set; }
        [StringLength(8, ErrorMessage = "语音平台名字请少于8个字")]
        public string? AudioPlatform { get; set; }
        [StringLength(20, ErrorMessage = "链接/号码长度请少于20字符")]
        public string? AudioLink { get; set; }

    }
}
