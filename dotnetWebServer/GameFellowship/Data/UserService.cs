namespace GameFellowship.Data
{
    public class UserService : IUserService
    {
        public List<User> Users { get; } = new() {
            new User("User 1", null, null, new int[]{1,2,3}, new int[]{1,2}, new int[]{1,2}, null),
            new User("User 2 with long", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1,2}, null),
            new User("User 3", null, null, null, null, new int[]{1,2}, null),
            new User("User 4", null, null, null, new int[]{3}, new int[]{3}, null),
            new User("User 55555555555", null, null, null, null, new int[]{1,2,3}, null),
            new User("User 6", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1}, null),
        };

        public string DefaultUserIconURI { get; } = "images/UserIcons/50913860_p9.jpg";
        public string DefaultUserName { get; } = "用户已注销";

        public async Task<bool> CreateNewUserAsync(UserModel user)
        {
            if (await HasUserAsync(user.UserName))
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(user.UserEmail) && await HasEmailAsync(user.UserEmail))
            {
                return false;
            }

            User newUser = new(user);
            Users.Add(newUser);

            return true;
        }

		public Task<string> GetUserNameAsync(int userID)
        {
            var resultUser =
                from user in Users
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
                from user in Users
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
                from user in Users
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
                from user in Users
                where user.UserID == userID
                select user;

            if (!resultUser.Any())
            {
                return Task.FromResult(Array.Empty<int>());
            }
            
            return Task.FromResult(resultUser.First().JoinedPostIDs.ToArray());
        }

        public Task<(string, string)> GetUserNameIconPairAsync(int userID)
        {
            var resultUser =
                from user in Users
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
                from user in Users
                where userIDs.Contains(user.UserID)
                select user;

            return Task.FromResult(resultUsers.ToDictionary(user => user.UserName, user => user.UserIconURI));
        }

        public Task<bool> HasUserAsync(int userID)
        {
            return Task.FromResult(Users.Any(user => user.UserID == userID));
        }

		public Task<bool> HasUserAsync(string userName)
        {
            return Task.FromResult(Users.Any(user => userName.Equals(user.UserName)));
        }

        public Task<bool> HasEmailAsync(string email)
        {
            return Task.FromResult(Users.Any(user => email.Equals(user.UserEmail)));
        }
    }
}