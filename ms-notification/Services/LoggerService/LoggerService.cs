
using ms_notification.Data;
using ms_notification.Models;
using ms_recip.Ms_recip.Models;
using System.Text.Json;

namespace ms_notification.Services.LoggerService;

public class LoggerService(DatabaseContext _databaseContext) : ILogger
{

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        try
        {
            // send message to log
        }
        catch (Exception ex)
        {
            Console.WriteLine("Log insertion problem");
            throw;
        }
    }
}
