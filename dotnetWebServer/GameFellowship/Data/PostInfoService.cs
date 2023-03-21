namespace GameFellowship.Data
{
    public class PostInfoService
    {
        private PostInfo[] posts = {
                new PostInfo(
                    1, new DateTime(2023, 2, 2, 2, 2, 2),
                    "Minecraft", "种地",
                    new string[] {"铁盔甲", "钻石剑", "自带干粮"},
                    "种土豆谢谢茄子",
                    5, 1, new int[] {1,2,3,5,6},
                    new DateTime(2023,3,21), new DateTime(2023,12,29),
                    "Kook", "https://www.baidu.com",
                    new Conversation[] {
                        new("user1", new DateTime(2023, 1, 11, 11, 11, 11), "Test, Test, \r\n, Long Context Test1"),
                        new("user2", new DateTime(2023, 2, 22, 22, 22, 22), "Test, Test, \r\n, Long Context Test1")
                    }
                ),
                new PostInfo(
                    2, new DateTime(2023, 1, 1, 1, 1, 1),
                    "Minecraft", "守村庄",
                    new string[] {"钻石甲", "钻石剑", "弩"},
                    "救救救救救救救救救救救",
                    5, 1, new int[] {1, 2,3,5}
                ),
                new PostInfo(
                    3, new DateTime(2023, 2, 2, 2, 2, 2),
                    "Destiny 2", "宝库大师Raid",
                    new string[] {"1810", "星火术", "速刷"},
                    "来打过的谢谢",
                    6, 4, new int[] {4,5},
                    null, null, null, null,
                    new Conversation[] {
                        new("user1", new DateTime(2022, 1, 11, 11, 11, 11), "Test, Test, \r\n, Long Context Test1"),
                        new("user3", new DateTime(2022, 2, 22, 22, 22, 22), "Test, Test, \r\n, Long Context Test1")
                    }
                ),
            };

        public Task<PostInfo> GetPostAsync(int id)
        {
            var resultPost = 
                from post in posts
                where post.PostID == id
                select post;

            if (resultPost.Count() == 0)
                return Task.FromResult(new PostInfo());

            return Task.FromResult(resultPost.First());
        }

        public Task<PostInfo[]> GetPostsGroupAsync(string game)
        {
            var resultPosts =
                from post in posts
                where post.GameName == game
                select post;

            return Task.FromResult(resultPosts.ToArray());
        }

        public Task<PostInfo[]> GetPostsGroupAsync(int[] postIDs)
        {
            var resultPosts =
                from post in posts
                where postIDs.Contains(post.PostID)
                select post;
            
            if (resultPosts.Count() == 0)
                return Task.FromResult(new PostInfo[]{});

            return Task.FromResult(resultPosts.ToArray());
        }
    }
}