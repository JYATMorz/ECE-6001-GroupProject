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

        public int LoginUserID { get; private set; } = -1;
        public bool UserHasLogin => LoginUserID > 0;

        public string DefaultUserIconURI { get; } = "images/UserIcons/50913860_p9.jpg";
        public string DefaultUserName { get; } = "用户已注销";

        public Task<string> GetUserNameAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (resultUser is not null && resultUser.Any())
            {
                return Task.FromResult(resultUser.First().UserName);
            }
            else
            {
                return Task.FromResult(DefaultUserName);
            }
        }

        public Task<string> GetUserIconURIAsync(int userID)
        {
            var resultUser = 
                from user in users
                where user.UserID == userID
                select user;

            if(resultUser is not null && resultUser.Any())
            {
                return Task.FromResult(resultUser.First().UserIconURI);
            }
            else
            {
                return Task.FromResult(DefaultUserIconURI);
            }
        }

        public Task<int[]> GetUserLikedGameIDsAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (!resultUser.Any())
            {
                return Task.FromResult(Array.Empty<int>());
            }

            return Task.FromResult(resultUser.First().LikedGameIDs.ToArray());
        }

        public Task<int[]> GetUserJoinedPostIDsAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (!resultUser.Any())
            {
                return Task.FromResult(Array.Empty<int>());
            }
            
            return Task.FromResult(resultUser.First().JoinedPostIDs.ToArray());
        }

        public Task<int[]> GetLoginUserJoinedPostIDsAsync()
        {
            if (!UserHasLogin)
            {
                return Task.FromResult(Array.Empty<int>());
            }

            return GetUserJoinedPostIDsAsync(LoginUserID);
        }

        public Task<(string, string)> GetUserNameIconPairAsync(int userID)
        {
            var resultUser =
                from user in users
                where user.UserID == userID
                select user;

            if (!resultUser.Any())
            {
                return Task.FromResult((string.Empty, string.Empty));
            }
            else
            {
                return Task.FromResult(
                    (resultUser.First().UserName, resultUser.First().UserIconURI));
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

        public Task<bool> UserLoginAsync(string userName, string password)
        {
            var resultUser = 
                from user in users
                where userName.Equals(user.UserName)
                select user;

            if (!resultUser.Any())
            {
                return Task.FromResult(false);
            }

            LoginUserID = resultUser.First().UserID;

            return Task.FromResult(true);
        }

        public Task<bool> UserLogoutAsync()
        {
            if (users.Any(user => user.UserID == LoginUserID))
            {
                LoginUserID = -1;
                return Task.FromResult(true);
            }

            return Task.FromResult(true);
        }
    }
}