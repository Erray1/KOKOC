using System.Text.Json.Serialization;

namespace KOKOC.ReverseProxy.Infrastructure.RolesSeeding
{
    public class RolesData
    {
        public string RoleName { get; set; }
        public List<AccountData>? Accounts { get; set; }
    }
}
