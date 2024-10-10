using KOKOC.ReverseProxy.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KOKOC.ReverseProxy.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppIdentityDbContext()
        {
            
        }
        public AppIdentityDbContext(DbContextOptions o) : base(o)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Database=KOKOC_Identity;Username=postgres;Password=admin");
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
