namespace DebtusTask.Models;

public class Shift
{
    public int Id { get; set; }

    public DateTime Started { get; set; }
    public DateTime? End { get; set; }
    public TimeSpan HoursWorked { get; set; }
    public bool Reprimand { get; set; }

    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}
