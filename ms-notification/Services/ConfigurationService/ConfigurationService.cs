using Microsoft.Extensions.Options;
using ms_configuration.Default;
using ms_configuration.Ms_configuration.Models;
using ms_notification.Models;
using ms_notification.Settings;

namespace ms_notification.Services.ConfigurationService;

public class ConfigurationService : IConfigurationService
{
    private readonly HttpClient _httpClient;
    private readonly MSConfigurationSettings _msConfigurationSettings;
    private readonly Container _odataContainer;

    public ConfigurationService(
        IHttpClientFactory httpClientFactory,
        IOptions<MSConfigurationSettings> msConfigurationSettingsOptions)
    {
        _httpClient = httpClientFactory.CreateClient();
        _msConfigurationSettings = msConfigurationSettingsOptions.Value;
        _odataContainer = new Container(new Uri(_msConfigurationSettings.OdataBaseUrl));
    }

    public async Task<MethodResult<RabbitMqConfigModel>> GetRabbitMqConfigAsync()
    {
        try
        {
            var rabbitMqConfig = _odataContainer.RabbitMqConfigs.OrderByDescending(c => c.CreationDate).FirstOrDefault();

            return MethodResult<RabbitMqConfigModel>.CreateSuccessResult(rabbitMqConfig);
        }
        catch (Exception ex)
        {
            return MethodResult<RabbitMqConfigModel>.CreateErrorResult(ex.Message);
        }
    }
}
