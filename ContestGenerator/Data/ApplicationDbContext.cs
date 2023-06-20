using ContestGenerator.Models.Contest;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Contest> Contests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
    }
}