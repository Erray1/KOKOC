using KOKOC.ReverseProxy.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KOKOC.ReverseProxy.Persistence.Endpoints
{
    [ApiController]
    [Route("api/telegram-auth")]
    public class TelegramAuthController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITelegramLoginValidator _telegramLoginValidator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ITelegramLoginClaimsConstructor _claimsConstructor;
        private readonly ITelegramLoginManager _loginManager;

        public TelegramAuthController(
            IServiceProvider serviceProvider,
            ITelegramLoginValidator telegramLoginValidator,
            IJwtTokenGenerator jwtTokenGenerator,
            ITelegramLoginManager loginManager,
            ITelegramLoginClaimsConstructor claimsConstructor)
        {
            _serviceProvider = serviceProvider;
            _telegramLoginValidator = telegramLoginValidator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _claimsConstructor = claimsConstructor;
            _loginManager = loginManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {

            // Проверяем подпись от Telegram
            if (_telegramLoginValidator.Validate(Request.Query))
            {
                return Unauthorized(new { Message = "Invalid Telegram signature" });
            }

            var claims = _claimsConstructor.BuildClaimsFromRequestQuery(Request.Query);
            var user = await _loginManager.LoginUser(claims);
            var roles = claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();
            var token = _jwtTokenGenerator.Generate(user, roles);

            // Возвращаем токен клиенту
            return Ok(new { Token = token });
        }
    }
}
