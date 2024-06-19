namespace DebtusTask.Models;

class Error(string errorMessage, object? @object = null)
{
    public string ErrorMessage { get; set; } = errorMessage;
    public object? Object { get; set; } = @object;
}