using Marry_Me.DTOs;
using Marry_Me.EF.Context;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Marry_Me.Services.Concrete.DataAccess
{
    public class AuthService(
        MarriageSystemDbContext context,
        IConfiguration configuration,
        IJWTService jwtService) : IAuthService
    {
        public async Task<AuthDataResponseDTO> Register(RegisterDTO request)
        {
            if (!new EmailAddressAttribute().IsValid(request.Email))
                return new AuthDataResponseDTO
                {
                    IsSuccessful = false,
                    Message = "Invalid email format."
                };

            if (await context.Users.AnyAsync(u => u.Email == request.Email))
                return new AuthDataResponseDTO
                {
                    IsSuccessful = false,
                    Message = "Email already exists."
                };

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var token = jwtService.GenerateToken(user);

            return new AuthDataResponseDTO
            {
                IsSuccessful = true,
                Message = "Registration successful",
                Data = new
                {
                    User = new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    }
                },
                Token = token
            };
        }

        public async Task<AuthDataResponseDTO> Login(LoginDTO request)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
                return new AuthDataResponseDTO
                {
                    IsSuccessful = false,
                    Message = "Invalid email or password. Please try again."
                };

            var result = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return new AuthDataResponseDTO
                {
                    IsSuccessful = false,
                    Message = "Invalid email or password. Please try again."
                };

            var token = jwtService.GenerateToken(user);

            return new AuthDataResponseDTO
            {
                IsSuccessful = true,
                Message = "Login successful",
                Data = new
                {
                    User = new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    }
                },
                Token = token,

            };
        }
    }
}