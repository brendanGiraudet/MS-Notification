using ms_configuration.Ms_configuration.Models;
using ms_notification.Models;

namespace ms_notification.Services.ConfigurationService;

public interface IConfigurationService
{
    Task<MethodResult<RabbitMqConfigModel>> GetRabbitMqConfigAsync();
}
