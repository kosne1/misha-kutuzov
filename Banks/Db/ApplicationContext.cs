using Banks.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banks.Db
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}