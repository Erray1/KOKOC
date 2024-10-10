using System.Security.Claims;

namespace KOKOC.ReverseProxy.Domain
{
    public interface ITelegramLoginClaimsConstructor
    {
        public List<Claim> BuildClaimsFromRequestQuery(IQueryCollection query);
    }
}
