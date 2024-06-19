using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebtusTask.Models;

namespace DebtusTask.Controllers;

[ApiController]
[Route("[controller]")]
public class HRDepartmentController(ApplicationContext db) : ControllerBase
{
    private readonly ApplicationContext db = db;

    [HttpGet("positions")]
    public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
    {
        return await db.Positions.ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        var employees = await db.Employees.Include(i => i.Shifts)
                                          .Include(i => i.Position)
                                          .ToListAsync();
        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetEmployeeId(int id)
    {
        var employee = await db.Employees.Include(i => i.Shifts)
                                         .Include(i => i.Position)
                                         .FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return BadRequest(new Error("Employee ID not found.", id));
        }

        return Ok(employee);
    }

    [HttpGet("{position}")]
    public async Task<ActionResult<Employee>> GetEmployeePosition(string position)
    {
        var employee = await db.Employees.Include(i => i.Position)
                                         .Include(i => i.Shifts)
                                         .FirstOrDefaultAsync(x => x.Position!.Name == position);
        if (employee == null)
        {
            return BadRequest(new Error("Position not found.", position));
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        if (employee == null
            || employee.Name == null
            || employee.Surname == null
            || employee.Position == null
            )
        {
            return BadRequest(new Error("The Name, Surname, Position fields must be filled in.", employee));
        }

        var position = await db.Positions.FirstOrDefaultAsync(p => p.Name == employee.Position.Name);
        if (position == null)
        {
            return BadRequest(new Error("Position not found.", employee.Position));
        }

        employee.Position = position;
        db.Employees.Add(employee);
        await db.SaveChangesAsync();
        return Ok(employee);
    }

    [HttpPut]
    public async Task<ActionResult<Employee>> PutEmployee(Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee must be filled.");
        }
        if (!db.Employees.Any(x => x.Id == employee.Id))
        {
            return BadRequest(new Error("Employee ID not found.", employee));
        }

        db.Update(employee);
        await db.SaveChangesAsync();
        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Employee>> DeleteEmployee(int id)
    {
        var employee = db.Employees.Include(i => i.Shifts)
                                   .Include(i => i.Position)
                                   .FirstOrDefault(x => x.Id == id);
        if (employee == null)
        {
            return BadRequest(new Error("Employee ID not found.", id));
        }

        db.Employees.Remove(employee);
        await db.SaveChangesAsync();
        return Ok(employee);
    }
}
