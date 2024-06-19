namespace DebtusTask.Models;

public class Position
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public TimeOnly DayStart { get; set; }
    public TimeOnly DayEnd { get; set; }
}
