using KOKOC.ReverseProxy.Application.Authentication;
using KOKOC.ReverseProxy.Domain;
using KOKOC.ReverseProxy.Domain.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KOKOC.ReverseProxy.Persistence.Endpoints
{
    public static class LoginViaVkEndpoint
    {
        public static WebApplication MapVKLogin(this WebApplication app)
        {
            app.MapPost("/signin-vk",
                async (HttpContext context,
                UserManager<User> userManager,
                RoleManager<User> roleManager,
                SignInManager<User> signInManager,
                JWTTokenGenerator jwtGenerator) =>
            {
                var result = await context.AuthenticateAsync(AppIdentityConstants.ProviderIDs.VKontakte);
                if (!result.Succeeded)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Ошибка авторизации");
                    context.Response.Redirect("/login");
                    return;
                }
                var vkId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var user = await userManager.FindByLoginAsync(AppIdentityConstants.ProviderIDs.VKontakte, vkId);

                if (user is null)
                {
                    user = new User
                    {
                        UserName = result.Principal.FindFirstValue(ClaimTypes.Name),
                        Email = result.Principal.FindFirstValue(ClaimTypes.Email),
                        Provider = AppIdentityConstants.ProviderIDs.VKontakte,
                    };
                    await userManager.CreateAsync(user);
                    await userManager.AddLoginAsync(user, new UserLoginInfo(
                        AppIdentityConstants.ProviderIDs.VKontakte,
                        vkId,
                        "Vkontakte"
                        ));
                    await roleManager.SetRoleNameAsync(user, "Base");
                }
                var roles = await userManager.GetRolesAsync(user);
                var jwtToken = jwtGenerator.Generate(user, roles);

                await context.Response.WriteAsJsonAsync(new {token = jwtToken});    
            });
            return app;
        }
    }
}
