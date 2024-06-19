using Microsoft.EntityFrameworkCore;

namespace DebtusTask.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Position> positions =
        [
            new() { Id = 1, Name = "Менеджер", },
            new() { Id = 2, Name = "Инженер", },
            new() { Id = 3, Name = "Тестировщик свечей", }
        ];

        modelBuilder.Entity<Position>().HasData(positions);
    }
}
