namespace GameFellowship.Data
{
    public class PostService
    {
        private PostModel[] posts = {
                new PostModel(
                    1, new DateTime(2023, 2, 2, 2, 2, 2),
                    "Minecraft", "种地",
                    new string[] {"铁盔甲", "钻石剑", "自带干粮"},
                    "种土豆谢谢茄子",
                    5, 1, new int[] {1,2,3,5,6},
                    new DateTime(2023,3,21), new DateTime(2023,12,29),
                    "Kook", "https://www.baidu.com",
                    new Conversation[] {
                        new(1, new DateTime(2023, 1, 11, 11, 11, 11), "Test, Test, \r\n, Long Context Test1"),
                        new(2, new DateTime(2023, 2, 22, 22, 22, 22), "Test, Test, \r\n, Long Context Test1")
                    }
                ),
                new PostModel(
                    2, new DateTime(2023, 1, 1, 1, 1, 1),
                    "Minecraft", "守村庄",
                    new string[] {"钻石甲", "钻石剑", "弩"},
                    "救救救救救救救救救救救",
                    5, 1, new int[] {1,2,3,5}
                ),
                new PostModel(
                    3, new DateTime(2023, 2, 2, 2, 2, 2),
                    "Destiny 2", "Raid",
                    new string[] {"1810", "星火术", "速刷"},
                    "来打过的谢谢",
                    6, 4, new int[] {4,5},
                    null, null, null, null,
                    new Conversation[] {
                        new(4, new DateTime(2022, 1, 11, 11, 11, 11), "Test, Test, \r\n, Long Context Test1"),
                        new(5, new DateTime(2022, 2, 22, 22, 22, 22), "Test, Test, \r\n, Long Context Test1")
                    }
                ),
            };

        public Task<PostModel> GetPostAsync(int id)
        {
            var resultPost =
                from post in posts
                where post.PostID == id
                select post;

            if (resultPost.Count() == 0)
                return Task.FromResult(new PostModel());

            return Task.FromResult(resultPost.First());
        }

        public Task<PostModel[]> GetPostsGroupAsync(string game)
        {
            var resultPosts =
                from post in posts
                where post.GameName == game
                select post;

            return Task.FromResult(resultPosts.ToArray());
        }

        public Task<PostModel[]> GetPostsGroupAsync(int[] postIDs)
        {
            var resultPosts =
                from post in posts
                where postIDs.Contains(post.PostID)
                select post;

            if (resultPosts.Count() == 0)
                return Task.FromResult(new PostModel[] { });

            return Task.FromResult(resultPosts.ToArray());
        }

        // TODO: Try add group by count
        public Task<string[]> GetMatchTypesAsync(int num, string? game = null)
        {
            IEnumerable<string> resultPosts;

            if (!string.IsNullOrWhiteSpace(game))
            {
                resultPosts = (
                    from post in posts
                    select post.MatchType
                    ).Take(num);
            }
            else
            {
                resultPosts = (
                    from post in posts
                    where post.GameName == game
                    select post.MatchType
                ).Take(num);
            }

            return Task.FromResult(resultPosts.ToArray());
        }
    }
}