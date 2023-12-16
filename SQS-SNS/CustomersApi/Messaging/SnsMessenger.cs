using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using CustomersApi.Models.Options;
using CustomersContracts;
using Microsoft.Extensions.Options;

namespace CustomersApi.Messaging;

public interface ISnsMessenger
{
    Task<PublishResponse> PublishMessageAsync<TMessage>(TMessage message) where TMessage : ISqsMessage;
}

public class SnsMessenger : ISnsMessenger
{
    private readonly IAmazonSimpleNotificationService _amazonSns;
    private readonly ILogger<SnsMessenger> _logger;
    private readonly IOptions<SNSOptions> _snsOptions;
    private string? _topicArn;

    public SnsMessenger(IAmazonSimpleNotificationService amazonSns, ILogger<SnsMessenger> logger, IOptions<SNSOptions> snsOptions)
    {
        _amazonSns = amazonSns;
        _logger = logger;
        _snsOptions = snsOptions;
    }

    public async Task<PublishResponse> PublishMessageAsync<TMessage>(TMessage message) where TMessage: ISqsMessage
    {
        var topicArn = await GetTopicArnAsync();
        var publishRequest = new PublishRequest
        {
            TopicArn = topicArn,
            Message = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    _snsOptions.Value.MessageTypeAttributeKey, new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(TMessage).Name
                    }
                }
            }
        };

        var result = await _amazonSns.PublishAsync(publishRequest);

        return result;
    }

    private async ValueTask<string?> GetTopicArnAsync()
    {
        if (_topicArn is not null)
        {
            return _topicArn;
        }

        var topicResponse = await _amazonSns.FindTopicAsync(_snsOptions.Value.TopicName);
        _topicArn = topicResponse.TopicArn;
        return _topicArn;
    }
}