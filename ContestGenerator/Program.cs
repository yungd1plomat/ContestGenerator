using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ContestGenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var db = services.GetRequiredService<ApplicationDbContext>();
                    var caddyApi = services.GetRequiredService<ICaddyApi>();

                    await db.Database.EnsureCreatedAsync().ConfigureAwait(true);

                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    await DataInitializer.SeedData(db, userManager, configuration).ConfigureAwait(true);
                    await CaddyInitializer.Initialize(db, caddyApi).ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            await host.RunAsync().ConfigureAwait(true);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}