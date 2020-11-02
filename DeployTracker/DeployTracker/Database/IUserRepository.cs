using DeployTracker.Models;


namespace DeployTracker.Database
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User GetUserByLoginPassword(string login, string password);
        void CreateUser(User user);
    }
}
