using Ardalis.Result;
using System.Security.Claims;

namespace KOKOC.ReverseProxy.Domain
{
    public interface ITelegramLoginManager
    {
        public Task<Result<User>> LoginUser(List<Claim> claims);
    }
}
