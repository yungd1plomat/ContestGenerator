using ContestGenerator.Data;
using Microsoft.AspNetCore.Identity;

namespace ContestGenerator.Helpers
{
    public static class DataInitializer
    {
        public static async Task SeedData(ApplicationDbContext db, 
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, configuration);
        }

        public static async Task SeedUsers(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            var requiredUser = await userManager.FindByNameAsync("admin");
            if (requiredUser is null)
            {
                var user = new AppUser()
                {
                    UserName = "admin",
                };
                var password = configuration["ADMIN_PASS"] ?? throw new InvalidOperationException("admin password not found");
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "admin");
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("user"))
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!await roleManager.RoleExistsAsync("jury"))
            {
                await roleManager.CreateAsync(new IdentityRole("jury"));
            }
        }
    }
}
