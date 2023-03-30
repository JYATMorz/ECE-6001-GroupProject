namespace GameFellowship.Data
{
    public class UserService : IUserService
    {
        private List<User> users = new() {
            new User("User 1", null, null, new int[]{1,2,3}, new int[]{1,2}, new int[]{1,2}, null),
            new User("User 2 with long", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1,2}, null),
            new User("User 3", null, null, null, null, new int[]{1,2}, null),
            new User("User 4", null, null, null, new int[]{3}, new int[]{3}, null),
            new User("User 55555555555", null, null, null, null, new int[]{1,2,3}, null),
            new User("User 6", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1}, null),
        };

        public Task<string?> GetUserNameAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (resultUser is not null && resultUser.Any())
            {
                return Task.FromResult<string?>(resultUser.First().UserIconURI);
            }
            else
            {
                return Task.FromResult<string?>(null);
            }
        }

        public Task<string?> GetUserIconURIAsync(int userID)
        {
            var resultUser = 
                from user in users
                where user.UserID == userID
                select user;

            if(resultUser is not null && resultUser.Any())
            {
                return Task.FromResult<string?>(resultUser.First().UserName);
            }
            else
            {
                return Task.FromResult<string?>(null);
            }
        }

        public Task<IEnumerable<int>?> GetUserLikedGameIDsAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (resultUser is not null && resultUser.Any())
            {
                return Task.FromResult<IEnumerable<int>?>(resultUser.First().LikedGameIDs);
            }
            else
            {
                return Task.FromResult<IEnumerable<int>?>(null);
            }
        }

        public Task<IEnumerable<int>?> GetUserJoinedPostIDsAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (resultUser is not null && resultUser.Any())
            {
                return Task.FromResult<IEnumerable<int>?>(resultUser.First().JoinedPostIDs);
            }
            else
            {
                return Task.FromResult<IEnumerable<int>?>(null);
            }
        }

        public Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userIDs)
        {
            var resultUsers = 
                from user in users
                where userIDs.Contains(user.UserID)
                select user;

            return Task.FromResult(resultUsers.ToDictionary(user => user.UserName, user => user.UserIconURI));
        }

        public Task<bool> HasUserAsync(int userID)
        {
            return Task.FromResult(users.Any(user => user.UserID == userID));
        }

    }
}