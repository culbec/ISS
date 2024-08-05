using Domain;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Task;

namespace Repository.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Boss> Bosses { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;

    public IDictionary<string, string>? Properties { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Properties is null)
        {
            return;
        }
        var connectionString = Properties["ConnectionString"];
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("users");

        modelBuilder.Entity<Employee>()
            .ToTable("employees")
            .HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Employee>(e => e.Uid)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Boss>()
            .ToTable("bosses")
            .HasOne(b => b.User)
            .WithOne()
            .HasForeignKey<Boss>(b => b.Uid)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}