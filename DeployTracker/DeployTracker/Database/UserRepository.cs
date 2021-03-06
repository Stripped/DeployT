﻿using DeployTracker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DeployTracker.Database
{

    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ILogger<UserRepository> logger)
        {
            List<User> people = new List<User>
            {
            new User {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new User { Login="qwerty@gmail.com", Password="55555", Role = "user" }
            };

            _logger = logger;
            _users.AddRange(people);
        }

        public void CreateUser(User user)
        {
            _users.Add(user);
        }

        public User GetUserById(int id)
        {
            try
            {
                return _users[id];
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
            return null;
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            try
            {
                var result = _users.Find(x => x.Login == login && x.Password == password);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
            return null;
        }
    }
}
