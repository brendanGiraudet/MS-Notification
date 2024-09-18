using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_notification.Models;

[Table("Notifications")]
public record NotificationModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string UserId { get; set; }
    
    public required string Content { get; set; }
    
    public required string ApplicationName { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; } = false;
    
    public bool Deleted { get; set; } = false;
}
