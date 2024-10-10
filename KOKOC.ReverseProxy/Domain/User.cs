using Microsoft.AspNetCore.Identity;

namespace KOKOC.ReverseProxy.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string? Provider { get; set; }
        public DateTime RegistredAt { get; set; } = DateTime.UtcNow;
        public string PhotoURL { get; set; }
    }
}
