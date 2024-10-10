using Ardalis.Result;
using KOKOC.ReverseProxy.Domain;
using KOKOC.ReverseProxy.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KOKOC.ReverseProxy.Application
{
    public class TelegramLoginManager : ITelegramLoginManager
    {
        private readonly UserManager<User> _userManager;
        public TelegramLoginManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<User>> LoginUser(List<Claim> claims)
        {
            var userId = claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByLoginAsync(AppIdentityConstants.ProviderIDs.Telegram, userId);

            if (user is null)
            {
                user = new User
                {
                    UserName = claims.Single(x => x.Type == ClaimTypes.Name).Value,
                    PhotoURL = claims.Single(x => x.Type == "photo").Value,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return Result.Error("Ошибка создания пользователя. Попробуйте позже");
                }
                var userLoginInfo = new UserLoginInfo
                (
                    loginProvider: AppIdentityConstants.ProviderIDs.Telegram,
                    providerKey: claims.Single(x => x.Type == userId).Value,
                    displayName: "Телеграм"
                );
                result = await _userManager.AddLoginAsync(user, userLoginInfo);
                if (!result.Succeeded)
                {
                    return Result.Error("Ошибка связывания аккаунта. Попробуйте позже");
                }

            }
            return Result.Success(user);
        }
    }
}
