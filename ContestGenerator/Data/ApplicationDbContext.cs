using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Contest> Contests { get; set; }

        public DbSet<Domain> Domains { get; set; }

        public DbSet<Response> Responses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}