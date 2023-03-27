namespace GameFellowship.Data
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<User[]> GetUserGroupAsync(int[] ids);
    }
}