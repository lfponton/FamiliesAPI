using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string username, string password);
    }
}