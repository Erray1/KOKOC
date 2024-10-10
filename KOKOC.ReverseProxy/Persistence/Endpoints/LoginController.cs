using KOKOC.ReverseProxy.Application.Authentication;
using KOKOC.ReverseProxy.Domain;
using KOKOC.ReverseProxy.Domain.Constants;
using KOKOC.ReverseProxy.Persistence.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.ReverseProxy.Persistence.Endpoints
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;
        public LoginController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtTokenGenerator;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Requests.LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await FindByLoginAsync(request.Login);
            if (user is null)
            {
                return Unauthorized(new { ErrorMessage = "Неверный логин или пароль" });
            }

            var isPasswordValidResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!isPasswordValidResult.Succeeded)
            {
                return Unauthorized(new { ErrorMessage = "Неверный логин или пароль" });
            }

            var token = _jwtGenerator.Generate(user, await _userManager.GetRolesAsync(user));
            await _signInManager.SignInAsync(user, true, AppIdentityConstants.ProviderIDs.Application);
            return Ok(new {Token = token});
        }
        private async Task<User?> FindByLoginAsync(string login)
        {
            var user = await _userManager.FindByEmailAsync(login);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(login);
            }
            return user;
        }
    }
}
