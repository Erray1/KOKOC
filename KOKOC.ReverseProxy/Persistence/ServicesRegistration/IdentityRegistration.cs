using KOKOC.ReverseProxy.Domain;
using KOKOC.ReverseProxy.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace KOKOC.ReverseProxy.Persistence.ServicesRegistration
{
    public static class IdentityRegistration
    {
        public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole<Guid>>()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>();
            return services;
        }
    }
}
