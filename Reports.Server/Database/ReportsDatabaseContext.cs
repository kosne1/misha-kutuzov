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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeModel>().ToTable("Employees");
        modelBuilder.Entity<TaskModel>().ToTable("Tasks");
        base.OnModelCreating(modelBuilder);
    }
}