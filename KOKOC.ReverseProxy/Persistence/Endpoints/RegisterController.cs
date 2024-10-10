using KOKOC.ReverseProxy.Application.Authentication;
using KOKOC.ReverseProxy.Domain;
using KOKOC.ReverseProxy.Domain.Constants;
using KOKOC.ReverseProxy.Persistence.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KOKOC.ReverseProxy.Persistence.Endpoints
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public RegisterController(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var user = await _userManager.FindByEmailAsync(request.Email);
            //if (user is not null)
            //{
            //    return BadRequest(new { ErrorMessage = "Данный email уже зарегистрирован" });
            //}
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                Provider = AppIdentityConstants.ProviderIDs.Application
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError()
                //}
                return BadRequest(new { Message = "Не удалось создать пользователя", Errors = result.Errors.ToList() });
            }
            await _userManager.AddToRoleAsync(user, AppIdentityConstants.RoleNames.BaseUser);
            return Ok();
        }
    }
}
    
