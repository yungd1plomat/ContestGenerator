using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Helpers
{
    public static class CaddyInitializer
    {
        public static async Task Initialize(ApplicationDbContext database, ICaddyApi caddyApi)
        {
            var domains = await database.Domains.ToListAsync();
            foreach (var domain in domains)
            {
                await caddyApi.AddNewRoute(domain.DomainName);
            }
        }
    }
}
