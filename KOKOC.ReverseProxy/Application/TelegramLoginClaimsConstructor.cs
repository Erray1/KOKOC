using KOKOC.ReverseProxy.Domain;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace KOKOC.ReverseProxy.Application
{
    public class TelegramLoginClaimsConstructor : ITelegramLoginClaimsConstructor
    {
        public List<Claim> BuildClaimsFromRequestQuery(IQueryCollection query)
        {
            var queryParams = query.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());

            bool queryHasId = queryParams.TryGetValue("id", out string? userid);
            if (!queryHasId)
            {
                throw new ArgumentException("Запрос не содержит id пользователя");
            }
            var firstName = queryParams["first_name"];
            var lastName = queryParams.TryGetValue("last_name", out string? ln) ? ln : string.Empty;
            var photoUrl = queryParams.TryGetValue("photo_url", out string? url) ? url : "Unknown";

            List<Claim> claims = [
                ];
            return claims;
        }
    }
}
