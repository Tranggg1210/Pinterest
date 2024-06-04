
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Data.Migrations;
using PixelPalette.Entities;

namespace PixelPalette.Extensions
{
    public class StartupTask : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await InitializeDataAsync();
            await AnalyseAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task InitializeDataAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                var mapper = services.GetRequiredService<IMapper>();
                await Seed.SeedUsers(userManager, roleManager, mapper);
            }
            catch (Exception) { }
        }

        private async Task AnalyseAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<PixelPaletteContext>();
                if (!(await context.Analysises.AnyAsync(a => a.CreateAt.Year == DateTime.Today.Year && a.CreateAt.Month == DateTime.Today.Month && a.CreateAt.Day == DateTime.Today.Day)))
                {
                    var analysis = new Entities.Analysis()
                    {
                        PostCount = await context.Posts.CountAsync(),
                        UserCount = await context.Users.CountAsync(),
                        NotificationCount = await context.Notifications.CountAsync(),
                        AccessCountInDay = 0,
                        CreateAt = DateTime.UtcNow
                    };
                    await context.Analysises.AddAsync(analysis);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception) { }
        }
    }
}
