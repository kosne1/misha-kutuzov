using Microsoft.EntityFrameworkCore;
using Reports.DAL.Models;
using Reports.DAL.Models.Tasks;

namespace Reports.Server.Database;

public class ReportsDatabaseContext : DbContext
{
    public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<EmployeeModel> Employees { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<Weekly> Weeklies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeModel>().ToTable("Employees");
        modelBuilder.Entity<TaskModel>().ToTable("Tasks");
        modelBuilder.Entity<Weekly>().ToTable("Weeklies");
        base.OnModelCreating(modelBuilder);
    }
}