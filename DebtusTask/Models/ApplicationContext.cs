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
        List<Position> positions = CreateDataHelper.Positions();
        List<Employee> employees = CreateDataHelper.Employees(positions);
        List<Shift> shifts = CreateDataHelper.Shifts(employees);

        modelBuilder.Entity<Position>().HasData(positions);
        modelBuilder.Entity<Employee>().HasData(employees);
        modelBuilder.Entity<Shift>().HasData(shifts);
    }

}
