using Microsoft.EntityFrameworkCore;
using ms_notification.Data;

namespace ms_notification.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyDatabaseMigrations(this WebApplication webApplication)
    {
        using var serviceScope = webApplication.Services.GetService<IServiceScopeFactory>()?.CreateScope();

        serviceScope?.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
    }
}