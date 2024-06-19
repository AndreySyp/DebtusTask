using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using DebtusTask.Models;

namespace DebtusTask;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
