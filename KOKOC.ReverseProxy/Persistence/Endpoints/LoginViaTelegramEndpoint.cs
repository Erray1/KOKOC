using KOKOC.ReverseProxy.Application.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace KOKOC.ReverseProxy.Persistence.Endpoints
{
    public static class LoginViaTelegramEndpoint
    {
        public static WebApplication MapTelegramLogin(this WebApplication app)
        {
            app.MapPost("/api/telegram-login", async (HttpContext context) =>
            {
                var request = context.Request;

                // Получаем параметры из запроса (Telegram их передает в строке запроса)
                var botToken = context.RequestServices.GetRequiredService<IConfiguration>()["Authentication:Telegram:BotToken"]!;

                if (!TelegramLoginValidator.Validate(request.Query, botToken))
                {
                    // Если подпись неверная, возвращаем ошибку
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Invalid signature");
                    return;
                }

                // Если валидация успешна, создаем ClaimsPrincipal для пользователя
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, request.Query["id"]!),
                    new Claim(ClaimTypes.Name, request.Query["first_name"]!),
                    new Claim("last_name",
                        request.Query.TryGetValue("last_name", out StringValues lastName)
                            ? lastName.ToString()
                            : string.Empty),
                    new Claim("photo_url",
                         request.Query.TryGetValue("last_name", out StringValues photoUrl)
                            ? photoUrl.ToString()
                            : throw new NotImplementedException("Добавь урл без фото"))
                };

                var identity = new ClaimsIdentity(claims, "Telegram");
                var principal = new ClaimsPrincipal(identity);

                // Выполняем аутентификацию пользователя и устанавливаем куку для сессии
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // После успешной аутентификации редиректим пользователя на основную страницу
                context.Response.Redirect("/");
            });
            return app;
        }
    }
}
