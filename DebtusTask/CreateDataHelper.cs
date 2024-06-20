using DebtusTask.Models;

namespace DebtusTask;

public static class CreateDataHelper
{
    private static readonly Random rnd = new();

    public static List<Position> Positions()
    {
        return [
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
    }
    public static List<Employee> Employees(List<Position> positions)
    {
        List<string> names = ["Андреев", "Ларин", "Алексеев", "Иванов", "Комиссаров", "Соколов", "Чернов", "Никитин", "Малышев", "Тарасов"];
        List<string> surname = ["Максим", "Леонид", "Иван", "Тимур", "Борис", "Михаил", "Владислав", "Михаил", "Тимур", "Даниил"];
        List<string> patronymic = ["Серафимович", "Георгиевич", "Михайлович", "Платонович", "Даниилович", "Максимович", "Маркович", "Богданович"];
        List<Employee> employees = [];

        for (int i = 1; i < 5; i++)
        {
            employees.Add(new()
            {
                Id = i,
                Name = names[rnd.Next(0, names.Count)],
                Surname = surname[rnd.Next(0, surname.Count)],
                Patronymic = patronymic[rnd.Next(0, patronymic.Count)],
                PositionId = positions[rnd.Next(0, positions.Count)].Id
            });
        }

        return employees;
    }
    public static List<Shift> Shifts(List<Employee> employees)

    {
        List<Shift> shifts = [];
        for (int i = 1; i < 10; i++)
        {
            int day = rnd.Next(1, 30);
            int hourStart = rnd.Next(1, 24);
            int minuteStart = rnd.Next(0, 60);
            int hourEnd = rnd.Next(hourStart, 24);
            int minuteEnd = rnd.Next(minuteStart, 60);

            var started = new DateTime(2024, 6, day, hourStart, minuteStart, 0);
            var end = new DateTime(2024, 6, day, hourEnd, minuteEnd, 0);

            shifts.Add(new()
            {
                Id = i,
                Started = started,
                End = end,
                Reprimand = rnd.NextDouble() >= 0.5,
                EmployeeId = employees[rnd.Next(0, employees.Count)].Id,
            });
        }

        return shifts;
    }
}
