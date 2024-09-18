using Microsoft.EntityFrameworkCore;
using ms_notification.Models;

namespace ms_notification.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<NotificationModel> Notifications { get; set; }
}
