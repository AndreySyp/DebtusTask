using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebtusTask.Models;

namespace DebtusTask.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckpointController(ApplicationContext db) : ControllerBase
{
    private readonly ApplicationContext db = db;

    [HttpPost("{id}/{startShift}")]
    public async Task<ActionResult<Shift>> StartShift(int id, DateTime startShift)
    {
        var employee = await db.Employees.Include(i => i.Shifts).FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return BadRequest(new Error("Employee ID not found.", id));
        }

        var shift = employee.Shifts.FirstOrDefault(x => x.End == null);
        if (shift != null)
        {
            return BadRequest(new Error("It is necessary to close the previous shift.", id));
        }

        db.Shifts.Add(new Shift()
        {
            Employee = employee,
            Started = startShift
        });
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}/{endShift}")]
    public async Task<ActionResult<Employee>> EndShift(int id, DateTime endShift)
    {
        var employee = await db.Employees.Include(i => i.Shifts)
                                         .Include(i => i.Position)
                                         .FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return BadRequest(new Error("Employee ID not found.", id));
        }

        var shift = employee.Shifts.FirstOrDefault(x => x.End == null);
        if (shift == null)
        {
            return BadRequest(new Error("It is necessary to open the previous shift.", id));
        }

        shift.End = endShift;
        shift.HoursWorked = endShift.Subtract(shift.Started);
        if (TimeOnly.FromDateTime(shift.Started) > employee.Position!.DayStart
            || TimeOnly.FromDateTime(endShift) < employee.Position!.DayEnd)
        {
            shift.Reprimand = true;
        }

        await db.SaveChangesAsync();
        return Ok();
    }
}
