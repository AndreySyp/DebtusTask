namespace DebtusTask.Models;

public class Shift
{
    private DateTime? end;

    public int Id { get; set; }

    public DateTime Started { get; set; }
    public DateTime? End
    {
        get => end;
        set
        {
            end = value;

            if (end is DateTime dt && Employee is Employee employee)
            {

                HoursWorked = dt.Subtract(Started);
                if (DateOnly.FromDateTime(Started) != DateOnly.FromDateTime(dt)
                    || TimeOnly.FromDateTime(Started) > employee.Position!.DayStart
                    || TimeOnly.FromDateTime(dt) < employee.Position!.DayEnd)
                {
                    Reprimand = true;
                }
            }
        }
    }
    public TimeSpan HoursWorked { get; private set; }
    public bool Reprimand { get; set; }

    public int? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}
