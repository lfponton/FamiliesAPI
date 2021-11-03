using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data.Impl
{
    public class WebUserService : IUserService
    {
        private readonly List<User> users;

        public WebUserService()
        {
            users = new[]
            {
                new User
                {
                    Id = 1,
                    Username = "Luis",
                    Password = "1234"
                    
                },
            }.ToList();
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User first = users.FirstOrDefault(user => user.Username.Equals(userName));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}