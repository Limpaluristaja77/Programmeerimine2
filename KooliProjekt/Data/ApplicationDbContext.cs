using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<Client> Clients { get; set; } 
        public DbSet<Materials> Materials { get; set; } 
        public DbSet<Panels> Panels { get; set; }
        public DbSet<Services> Services { get; set; }

    }
}