namespace ms_notification.Constants;

public static class RabbitmqConstants
{
    public const string ApplicationName = "ms-notification";

    // EXCHANGE
    public const string RecipExchangeName = "recip";
    public const string NotificationExchangeName = "notification";

    // RECIP
    public const string CreateRecipResultRoutingKey = "CreateRecipResult";
    
    public const string UpdateRecipResultRoutingKey = "UpdateRecipResult";
    
    public const string DeleteRecipResultRoutingKey = "DeleteRecipResult";
    
    // NOTIFICATION
    public const string DeleteNotificationRoutingKey = "DeleteNotification";
}
