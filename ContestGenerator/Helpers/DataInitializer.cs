using ContestGenerator.Data;
using Microsoft.AspNetCore.Identity;

namespace ContestGenerator.Helpers
{
    public static class DataInitializer
    {
        public static async Task SeedData(ApplicationDbContext db, 
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration)
        {
            await SeedUsers(userManager, configuration);
        }

        public static async Task SeedUsers(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var requiredUser = await userManager.FindByNameAsync("admin");
            if (requiredUser is null)
            {
                var user = new IdentityUser("admin");
                var password = configuration["ADMIN_PASS"] ?? throw new InvalidOperationException("admin password not found");
                await userManager.CreateAsync(user, password);
            }
        }
    }
}
