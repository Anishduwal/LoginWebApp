using Login.Application.DTOs;
using Login.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }

        //for authorization test
        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("JWT Protected Data");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ResponseRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshAsync(request.RefreshToken);
            if (result == null)
                return Unauthorized("Invalid refresh token");

            return Ok(result);
        }

    }
}
