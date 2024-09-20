using ms_configuration.Ms_configuration.Models;
using ms_notification.Constants;
using ms_notification.Models;
using ms_notification.Services.ConfigurationService;
using ms_notification.Services.NotificationsService;
using ms_recip.Ms_recip.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ms_notification.Services.RabbitMq;

public class RabbitMqSubscriberService : IHostedService, IDisposable
{
    private readonly IConfigurationService _configurationService;
    private readonly Timer _timer;
    private IModel _channel;
    private IConnection _connection;
    private string _queueName;
    private readonly int _pollingInterval;
    private readonly IServiceProvider _serviceProvider;
    private RabbitMqConfigModel? _rabbitMqConfigModel;

    public RabbitMqSubscriberService(
        IConfigurationService configurationService,
        IConfiguration configuration,
        IServiceProvider serviceProvider)
    {
        _configurationService = configurationService;
        _pollingInterval = int.Parse(configuration["RabbitMQ:PollingInterval"] ?? "5000");

        _timer = new Timer(ProcessMessages, null, Timeout.Infinite, _pollingInterval);

        _serviceProvider = serviceProvider;

        _queueName = configuration["RabbitMqQueueName"];
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var rabbitMqConfigResult = await _configurationService.GetRabbitMqConfigAsync();

        if (!rabbitMqConfigResult.IsSuccess)
            return;

        _rabbitMqConfigModel = rabbitMqConfigResult.Value;

        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqConfigModel.Hostname,
            Port = _rabbitMqConfigModel.Port,
            UserName = _rabbitMqConfigModel.Username,
            Password = _rabbitMqConfigModel.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        _timer.Change(0, _pollingInterval);
    }

    private void ProcessMessages(object state)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message from {_queueName}: {message}");

            if (ea.Exchange == RabbitmqConstants.RecipExchangeName)
            {
                await HandleRecipAsync(message, ea.RoutingKey);
            }
            else if (ea.Exchange == RabbitmqConstants.NotificationExchangeName)
            {
                await HandleNotificationAsync(message, ea.RoutingKey);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }

    private async Task HandleNotificationAsync(string message, string routingKey)
    {
        try
        {
            var deserializedMessage = JsonSerializer.Deserialize<RabbitMqMessageBase<NotificationModel>>(message);

            if (deserializedMessage != null)
            {
                using var scope = _serviceProvider.CreateScope();

                var notificationsService = scope.ServiceProvider.GetRequiredService<INotificationsService>();

                switch (routingKey)
                {
                    case RabbitmqConstants.DeleteNotificationRoutingKey:
                        {
                            await notificationsService.DeleteAsync(deserializedMessage.Payload);
                        }
                        break;

                    default:
                        
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du traitement du message: {ex.Message}");
        }
    }
    
    private async Task HandleRecipAsync(string message, string routingKey)
    {
        try
        {
            var deserializedMessage = JsonSerializer.Deserialize<RabbitMqMessageBase<RecipModel>>(message);

            if (deserializedMessage != null)
            {
                using var scope = _serviceProvider.CreateScope();

                var notificationsService = scope.ServiceProvider.GetRequiredService<INotificationsService>();

                string notificationContent;

                switch (routingKey)
                {
                    case RabbitmqConstants.CreateRecipResultRoutingKey:
                        {
                            notificationContent = $"The recip {deserializedMessage.Payload.Name} has been created";
                        }
                        break;

                    case RabbitmqConstants.UpdateRecipResultRoutingKey:
                        {
                            notificationContent = $"The recip {deserializedMessage.Payload.Name} has been updated";
                        }
                        break;

                    case RabbitmqConstants.DeleteRecipResultRoutingKey:
                        {
                            notificationContent = $"The recip {deserializedMessage.Payload.Name} has been deleted";
                        }
                        break;

                    default:
                        notificationContent = string.Empty;
                        break;
                }

                if (!string.IsNullOrEmpty(notificationContent))
                {
                    await notificationsService.CreateAsync(new NotificationModel
                    {
                        ApplicationName = deserializedMessage.ApplicationName,
                        Content = notificationContent,
                        UserId = deserializedMessage.UserId,
                        Timestamp = DateTime.UtcNow,
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du traitement du message: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        _channel?.Close();
        _connection?.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
