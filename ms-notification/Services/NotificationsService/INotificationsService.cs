using ms_notification.Models;

namespace ms_notification.Services.NotificationsService;

public interface INotificationsService
{
    /// <summary>
    /// Create notification 
    /// </summary>
    /// <param name="notificationModel"></param>
    /// <returns></returns>
    Task<MethodResult<NotificationModel>> CreateAsync(NotificationModel notificationModel);
    
    /// <summary>
    /// Delete notification
    /// </summary>
    /// <param name="notificationModel"></param>
    /// <returns></returns>
    Task<MethodResult<NotificationModel>> DeleteAsync(NotificationModel notificationModel);
}
