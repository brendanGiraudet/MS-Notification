using ms_notification.Models;

namespace ms_notification.Services.NotificationsService;

public class NotificationsService : INotificationsService
{
    /// <inheritdoc/>
    public Task<MethodResult<NotificationModel>> CreateAsync(NotificationModel notificationModel)
    {
        throw new NotImplementedException();
    }
}
