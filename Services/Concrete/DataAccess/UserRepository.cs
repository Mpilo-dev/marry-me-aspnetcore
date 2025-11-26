using Marry_Me.DTOs;
using Marry_Me.EF.Context;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Marry_Me.Services.Concrete.DataAccess
{
    public class UserRepository(MarriageSystemDbContext context) : IUserRepository
    {
        private readonly MarriageSystemDbContext _context = context;

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }

}
