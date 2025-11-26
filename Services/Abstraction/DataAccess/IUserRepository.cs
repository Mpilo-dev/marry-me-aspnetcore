using Marry_Me.DTOs;
using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.DataAccess
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<bool> EmailExists(string email);
    }
}
