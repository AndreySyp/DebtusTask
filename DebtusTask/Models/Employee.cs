namespace DebtusTask.Models;

public class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }

    public Position? Position { get; set; }
    public List<Shift> Shifts { get; set; } = [];
}
