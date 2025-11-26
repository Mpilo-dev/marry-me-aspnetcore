using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction
{   
    public interface IJWTService
    {
        string GenerateToken(User user);
    }
}
