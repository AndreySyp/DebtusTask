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
            new()
            {
                Id = 1,
                Name = "Менеджер",
                DayStart = new TimeOnly(9, 00),
                DayEnd = new TimeOnly(18, 00),
            },
            new()
            {
                Id = 2,
                Name = "Инженер",
                DayStart = new TimeOnly(9, 00),
                DayEnd = new TimeOnly(18, 00),
            },
            new()
            {
                Id = 3,
                Name = "Тестировщик свечей",
                DayStart = new TimeOnly(9, 00),
                DayEnd = new TimeOnly(21, 00),
            }
        ];

        modelBuilder.Entity<Position>().HasData(positions);
    }
}
