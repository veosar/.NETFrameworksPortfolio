using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using CustomersContracts;
using CustomersNotificationsService.Models.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace CustomersNotificationsService.HostedServices;

public class QueueConsumerService : BackgroundService
{
    private readonly IOptions<QueueOptions> _queueOptions;
    private readonly ILogger<QueueConsumerService> _logger;
    private readonly IAmazonSQS _amazonSqs;
    private readonly List<string> _attributeNames = ["All"];
    private string? _queueUrl;
    private readonly Dictionary<string, Type> _messageTypes;
    private IServiceScopeFactory _serviceScopeFactory;
    
    public QueueConsumerService(IOptions<QueueOptions> queueOptions, IAmazonSQS amazonSqs, ILogger<QueueConsumerService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _queueOptions = queueOptions;
        _amazonSqs = amazonSqs;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _messageTypes = typeof(ISqsMessage).Assembly.DefinedTypes
            .Where(x =>
                typeof(ISqsMessage).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .ToDictionary(type => type.Name, type => type.AsType());
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await GetQueueUrlAsync();
        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            AttributeNames = _attributeNames,
            MessageAttributeNames = _attributeNames,
            MaxNumberOfMessages = 5
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _amazonSqs.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);
            foreach (var message in response.Messages)
            {
                var messageType = message.MessageAttributes[_queueOptions.Value.MessageTypeAttributeKey].StringValue;
                if (messageType is null)
                {
                    _logger.LogInformation("It is not possible to consume message with id: {messageId} because it does not have {messageTypeAttributeKey} key in MessageAttributes", message.MessageId, _queueOptions.Value.MessageTypeAttributeKey);
                    continue;
                }
                
                var type = _messageTypes.GetValueOrDefault(messageType);

                if (type is null)
                {
                    _logger.LogInformation("It is not possible to consume message with id: {messageId} and MessageType: {messageType}, because this type is not registered", message.MessageId, messageType);
                    continue;
                }
                
                _logger.LogInformation("Started processing message with id {messageId}", message.MessageId);

                var typedMessage = (ISqsMessage)JsonSerializer.Deserialize(message.Body, type)!;

                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(typedMessage, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error ocurred during processing of message with id {messageId}", message.MessageId);
                    continue;
                }
                
                await _amazonSqs.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, stoppingToken);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async ValueTask<string> GetQueueUrlAsync()
    {
        if (_queueUrl is not null)
        {
            return _queueUrl;
        }
        var queueUrlResponse = await _amazonSqs.GetQueueUrlAsync(_queueOptions.Value.Name);
        _queueUrl = queueUrlResponse.QueueUrl;
        return _queueUrl;
    }
}