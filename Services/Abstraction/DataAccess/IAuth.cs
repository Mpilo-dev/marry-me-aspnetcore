using Marry_Me.DTOs;
using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.DataAccess
{
    public interface IAuthService
    {
        Task<AuthDataResponseDTO> Register(RegisterDTO request);
        Task<AuthDataResponseDTO> Login(LoginDTO request);
    }
}
