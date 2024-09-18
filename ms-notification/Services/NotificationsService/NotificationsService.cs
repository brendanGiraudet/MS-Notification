using ms_notification.Data;
using ms_notification.Models;

namespace ms_notification.Services.NotificationsService;

public class NotificationsService(DatabaseContext _context) : INotificationsService
{
    /// <inheritdoc/>
    public async Task<MethodResult<NotificationModel>> CreateAsync(NotificationModel notificationModel)
    {
        try
        {
            _context.Notifications.Add(notificationModel);
            _context.SaveChanges();

            return MethodResult<NotificationModel>.CreateSuccessResult(notificationModel);
        }
        catch (Exception ex)
        {
            return MethodResult<NotificationModel>.CreateErrorResult(ex.Message);
        }
    }
}
