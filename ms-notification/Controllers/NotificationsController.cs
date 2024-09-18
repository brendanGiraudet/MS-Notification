using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ms_notification.Data;
using ms_notification.Models;

namespace ms_notification.Controllers;

public class NotificationsController(DatabaseContext context) : ODataController
{
    private readonly DatabaseContext _context = context;

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Notifications);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] Guid key)
    {
        var config = _context.Notifications.FirstOrDefault(c => c.Id == key);
        if (config == null)
        {
            return NotFound();
        }
        return Ok(config);
    }

    [HttpPost]
    public IActionResult Post([FromBody] NotificationModel notification)
    {
        _context.Notifications.Add(notification);
        _context.SaveChanges();

        return Created(notification);
    }
    
    [HttpPatch]
    public IActionResult Patch([FromODataUri] Guid key, [FromBody] NotificationModel notification)
    {
        if(key != notification.Id) return BadRequest();

        _context.Notifications.Update(notification);
        _context.SaveChanges();

        return Ok(notification);
    }
}
