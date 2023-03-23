namespace GameFellowship.Data
{
    public class UserService
    {
        private UserModel[] users = {
            new UserModel(1, "User 1", null, null, new int[]{1,2,3}, new int[]{1,2}, new int[]{1,2}, null),
            new UserModel(2, "User 2 with long", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1,2}, null),
            new UserModel(3, "User 3", null, null, null, null, new int[]{1,2}, null),
            new UserModel(4, "User 4", null, null, null, new int[]{3}, new int[]{3}, null),
            new UserModel(5, "User 55555555555", null, null, null, null, new int[]{1,2, 3}, null),
            new UserModel(6, "User 6", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1}, null),
        };

        public Task<UserModel> GetUserAsync(int id)
        {
            var resultUser =
                from user in users
                where user.UserID == id
                select user;

            if (resultUser.Count() == 0)
                return Task.FromResult(new UserModel());

            return Task.FromResult(resultUser.First());
        }

        public Task<UserModel[]> GetUserGroupAsync(int[] ids)
        {
            var resultUser =
                from user in users
                where ids.Contains(user.UserID)
                select user;

            if (resultUser.Count() == 0)
                return Task.FromResult(new UserModel[] { });

            return Task.FromResult(resultUser.ToArray());
        }
    }
}