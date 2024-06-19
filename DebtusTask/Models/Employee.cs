using System.ComponentModel.DataAnnotations.Schema;

namespace DebtusTask.Models;

public class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? AmountReprimands 
    { 
        get
        {
            return Shifts.Where(s => s.Reprimand && DateTime.Now.Month == s.Started.Month).Count();
        }
        private set { } 
    }

    public Position? Position { get; set; }
    public List<Shift> Shifts { get; set; } = [];
}
