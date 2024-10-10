using KOKOC.ReverseProxy.Domain;
using KOKOC.ReverseProxy.Infrastructure.RolesSeeding;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace KOKOC.ReverseProxy.Infrastructure.Seeding
{
    public class SeedingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _lifetime;
        public SeedingService(
            IHostApplicationLifetime lifetime,
            IServiceProvider serviceProvider)
        {
            _lifetime = lifetime;
            _serviceProvider = serviceProvider;
            _lifetime.ApplicationStarted.Register(async () => await InitializeRoles());
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
        private async Task InitializeRoles()
        {
            using var scope = _serviceProvider.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();


            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var roleData = config.GetSection("RolesSeeding").Get<List<RolesData>>();
            //var roleData = JsonSerializer.Deserialize<List<RolesData>>(roleDataString);

            foreach (var role in roleData)
            {
                if (!await roleManager.RoleExistsAsync(role.RoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role.RoleName));
                }
                if (role.Accounts is null) continue;
                foreach (var userData in role.Accounts)
                {
                    User? user = await userManager.FindByEmailAsync(userData.Login);
                    if (user is null)
                    {
                        User? newUser = new User
                        {
                            Email = userData.Login,
                            UserName = userData.UserName,
                            PasswordHash = ""
                        };
                        var result = await userManager.CreateAsync(newUser);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newUser, role.RoleName);
                        }

                    }
                }
            }
        }
    }
}
