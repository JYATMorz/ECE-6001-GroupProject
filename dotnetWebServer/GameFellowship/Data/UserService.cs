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

        public Task<User> GetUserAsync(int id)
        {
            var resultUser =
                from user in users
                where user.UserID == id
                select user;

            if (!resultUser.Any())
                return Task.FromResult(new User());

            return Task.FromResult(resultUser.First());
        }

        public Task<User[]> GetUserGroupAsync(int[] ids)
        {
            var resultUser =
                from user in users
                where ids.Contains(user.UserID)
                select user;

            if (!resultUser.Any())
                return Task.FromResult(Array.Empty<User>());

            return Task.FromResult(resultUser.ToArray());
        }
    }
}