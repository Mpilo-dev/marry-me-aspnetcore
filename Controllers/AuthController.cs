
using Marry_Me.DTOs;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Marry_Me.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthDataResponseDTO>> Register(RegisterDTO request)
        {
            var response = await authService.Register(request);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDataResponseDTO>> Login(LoginDTO request)
        {
            var response = await authService.Login(request);
            return response.IsSuccessful ? Ok(response) : Unauthorized(response);
        }
    }
}