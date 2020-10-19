using DeployTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Database
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User GetUserByLoginPassword(string login, string password);
        void CreateUser(User user);
    }
}
