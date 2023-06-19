using ContestGenerator.Data;
using Microsoft.AspNetCore.Identity;

namespace ContestGenerator.Helpers
{
    public static class DataInitializer
    {
        public static async Task SeedData(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            await SeedUsers(userManager);
        }

        public static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            var requiredUser = await userManager.FindByNameAsync("admin");
            if (requiredUser is null)
            {
                var user = new IdentityUser("admin");
                await userManager.CreateAsync(user, "Admin_1234");
            }
        }
    }
}
